using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University_students.Models;
using Microsoft.Office.Interop.Word;
using System.Threading;

namespace University_students.Services
{
    class TeacherDocs
    {
        public static void CreateReport(TaughtGroups taughtGroups)
        {
            CustomBoxes.CustomMessageBox msgbox = new CustomBoxes.CustomMessageBox("Waiting");
            msgbox.Show();
            try
            {

                Application appWord = new Application();
                appWord.ShowAnimation = false;
                appWord.Visible = false;
                object missing = System.Reflection.Missing.Value;
                Document document = appWord.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                foreach (Section section in document.Sections)
                {
                    Range headerRange = section.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, WdFieldType.wdFieldPage);
                    headerRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    headerRange.Font.ColorIndex = WdColorIndex.wdBlack;
                    headerRange.Font.Size = 25;
                    headerRange.Text = $"Отчет за {DateTime.Today.ToString("MM.")}";
                }

                foreach (Section wordSection in document.Sections)
                {
                    Range footerRange = wordSection.Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    footerRange.Font.ColorIndex = WdColorIndex.wdBlack;
                    footerRange.Font.Size = 20;
                    footerRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    footerRange.Text = $"Университет: {taughtGroups.Teaching.User.Pulpit.Faculty.University.Name}";
                }

                document.Content.SetRange(0, 0);
                document.Content.Text = 
                      $"Преподаватель: {taughtGroups.Teaching.User}" 
                    + Environment.NewLine
                    + $"Предмет: {taughtGroups.Subject.Name}"
                    + Environment.NewLine
                    + $"Группа: {taughtGroups.Group}"
                    + Environment.NewLine;

                Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                Thread.Sleep(1000);
                para1.Range.InsertParagraphAfter();

                Table firstTable = document.Tables.Add(para1.Range, taughtGroups.Group.Students.Count + 1, 8, ref missing, ref missing);
                firstTable.Borders.Enable = 1;
                User[] students = taughtGroups.Group.Students.ToArray();
                int numStudent = 0;
                foreach(Row row in firstTable.Rows)
                {
                    SubjectProgress currentSubjectProgress = GetSubjectProgress(taughtGroups, students[numStudent]);
                    foreach (Cell cell in row.Cells)
                    {
                        if (cell.RowIndex == 1)
                        {
                            if (cell.ColumnIndex == 1) FillCell(cell, "Студент");
                            if (cell.ColumnIndex == 2) FillCell(cell, "1ая аттестация");
                            if (cell.ColumnIndex == 3) FillCell(cell, "2ая аттестация");
                            if (cell.ColumnIndex == 4) FillCell(cell, "Зачет");
                            if (cell.ColumnIndex == 5) FillCell(cell, "Экзамен");
                            if (cell.ColumnIndex == 6) FillCell(cell, "По уважительной");
                            if (cell.ColumnIndex == 7) FillCell(cell, "По неуважительной");
                            if (cell.ColumnIndex == 8) FillCell(cell, "Количество отработок");
                        }
                        else
                        {

                            switch (cell.ColumnIndex)
                            {
                                case 1:
                                    cell.Range.Text = students[numStudent].ToString();
                                    break;
                                case 2:
                                    cell.Range.Text = CheckCertification(currentSubjectProgress, false);
                                    break;
                                case 3:
                                    cell.Range.Text = CheckCertification(currentSubjectProgress, true);
                                    break;
                                case 4:
                                    cell.Range.Text = CheckOffset(currentSubjectProgress);
                                    break;
                                case 5:
                                    cell.Range.Text = CheckExam(currentSubjectProgress);
                                    break;
                                case 6:
                                    cell.Range.Text = currentSubjectProgress.ValidExcuses.ToString();
                                    break;
                                case 7:
                                    cell.Range.Text = currentSubjectProgress.UnValidExcuses.ToString();
                                    break;
                                case 8:
                                    cell.Range.Text = currentSubjectProgress.WorkOuts.Count.ToString();
                                    break;
                            }
                        }
                    }
                    if (row.Index != 1) numStudent++;
                }
                object filename = $"qwe123_report.docx";
                document.SaveAs2(ref filename);
                document.Close(ref missing, ref missing, ref missing);
                document = null;
                appWord.Quit(ref missing, ref missing, ref missing);
                appWord = null;
                new CustomBoxes.CustomMessageBox("Complete").Show();
                msgbox.Close();
            }
            catch (Exception ex)
            {
                msgbox.Close();
                new CustomBoxes.CustomMessageBox(ex.Message).Show();
            }
        }

        private static void FillCell(Cell cell, string str)
        {
            cell.Range.Text = str;
            cell.Range.Font.Bold = 2;
            cell.Range.Font.Name = "verdana";
            cell.Range.Font.Size = 10;
            cell.Shading.BackgroundPatternColor = WdColor.wdColorGray25;
            cell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
        }

        private static SubjectProgress GetSubjectProgress(TaughtGroups taughtGroups, User student)
        {
            using (USDbContext db = new USDbContext())
            {
                return db.SubjectProgress.Include("WorkOuts").FirstOrDefault(sp => sp.TaughtGroupsId == taughtGroups.Id && sp.UserId == student.Id);
            }
        }

        private static string CheckExam(SubjectProgress subpro)
        {
            switch(subpro.IsExamPassed)
            {
                case Enums.StateExam.Failed:
                    return "Завален";
                case Enums.StateExam.Waiting:
                    return "В ожидание";
                case Enums.StateExam.Passed:
                    return "Сдан";
                case Enums.StateExam.Retake:
                    return "Пересдача";
                default:
                    return "";
            }
        }

        private static string CheckOffset(SubjectProgress subpro)
        {
            if (subpro.IsOffsetPassed)
                return "Зачет";
            else
                return "Не сдан";
        }

        private static string CheckCertification(SubjectProgress subpro, bool IsLastCerification)
        {
            if (IsLastCerification)
                return StatusCerification(subpro.IsFinishCertifiationPassed);
            else
                return StatusCerification(subpro.IsStartCertifiationPassed);
        }

        private static string StatusCerification(Enums.StateCertification state)
        {
            string res = String.Empty;
            switch (state)
            {
                case Enums.StateCertification.Failed:
                    res = "Не аттестован";
                    break;
                case Enums.StateCertification.Passed:
                    res = "Aттестован";
                    break;
                case Enums.StateCertification.Waiting:
                    res = "Еще не аттестован";
                    break;
            }
            return res;
        }

    }
}
