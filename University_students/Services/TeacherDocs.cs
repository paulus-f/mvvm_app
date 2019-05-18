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
                    headerRange.Text = $"Отчет за {DateTime.Today.ToString("dd.MM.yyyy")}";
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
                document.PageSetup.LeftMargin = 4.0F;
                Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                para1.Range.InsertAfter("");
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
                            if (cell.ColumnIndex == 1) FillCell(cell, 80, "Студент");
                            if (cell.ColumnIndex == 2) FillCell(cell, 90,"1ая аттестация");
                            if (cell.ColumnIndex == 3) FillCell(cell, 90, "2ая аттестация");
                            if (cell.ColumnIndex == 4) FillCell(cell, 80, "Зачет");
                            if (cell.ColumnIndex == 5) FillCell(cell, 90, "Экзамен");
                            if (cell.ColumnIndex == 6) FillCell(cell, 50, "По уважительной");
                            if (cell.ColumnIndex == 7) FillCell(cell, 50, "По неуважительной");
                            if (cell.ColumnIndex == 8) FillCell(cell, 50, "Количество отработок");
                        }
                        else
                        {

                            switch (cell.ColumnIndex)
                            {
                                case 1:
                                    cell.Width = 80;
                                    cell.Range.Font.Size = 8;
                                    cell.Range.Text = students[numStudent].ToString();
                                    break;
                                case 2:
                                    cell.Width = 90;
                                    cell.Range.Font.Size = 8;
                                    cell.Range.Text = CheckCertification(currentSubjectProgress, false);
                                    break;
                                case 3:
                                    cell.Width = 90;
                                    cell.Range.Font.Size = 8;
                                    cell.Range.Text = CheckCertification(currentSubjectProgress, true);
                                    break;
                                case 4:
                                    cell.Width = 80;
                                    cell.Range.Text = CheckOffset(currentSubjectProgress);
                                    break;
                                case 5:
                                    cell.Width = 90;
                                    cell.Range.Text = CheckExam(currentSubjectProgress);
                                    break;
                                case 6:
                                    cell.Width = 50;
                                    cell.Range.Text = currentSubjectProgress.ValidExcuses.ToString();
                                    break;
                                case 7:
                                    cell.Width = 50;
                                    cell.Range.Text = currentSubjectProgress.UnValidExcuses.ToString();
                                    break;
                                case 8:
                                    cell.Width = 50;
                                    cell.Range.Text = currentSubjectProgress.WorkOuts.Count.ToString();
                                    numStudent++;
                                    break;
                            }
                        }
                    }
                }
                object filename = $"report_{DateTime.Now.ToString("dd_M_yyyy_hh_mm")}.docx";
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

        private static void FillCell(Cell cell, int width, string str)
        {
            cell.Width = width;
            cell.Range.Text = str;
            cell.Range.Font.Bold = 2;
            cell.Range.Font.Name = "verdana";
            cell.Range.Font.Size = 8;
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
