using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University_students.Models;

namespace University_students.Services
{
    class StudentsDocs
    {
        public static void CreateReport(SubjectProgress sp)
        {
            Faculty faculty = sp.TaughtGroups.Group.Speciality.Faculty;

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
                    headerRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                    headerRange.Font.ColorIndex = WdColorIndex.wdBlack;
                    headerRange.Font.Size = 25;
                    headerRange.Text = 
                          $"Декану факультета {faculty.Name}"
                        + Environment.NewLine
                        + faculty.Dean
                        + Environment.NewLine
                        + $"Студента(ки) {DateTime.Today.Year - sp.TaughtGroups.Group.FirstYear}, группы {sp.TaughtGroups.Group.NumberGroup}"
                        + Environment.NewLine
                        + sp.User;
                }

                foreach (Section section in document.Sections)
                {
                    Range mainPart = section.Parent.Range;
                    mainPart.Fields.Add(mainPart, WdFieldType.wdFieldPage);
                    mainPart.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    mainPart.Font.ColorIndex = WdColorIndex.wdBlack;
                    mainPart.Font.Size = 20;
                    mainPart.Text =
                          "ЗАЯВЛЕНИЕ"
                        + Environment.NewLine
                        + "Прошу разрешить отработать лабораторную(ые) работу(ы) по предмету"
                        + Environment.NewLine
                        + $" {sp.TaughtGroups.Subject} (преподаватель {sp.TaughtGroups.Teaching.User})"
                        + Environment.NewLine;
                }

                foreach (Section wordSection in document.Sections)
                {
                    Range footerRange = wordSection.Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    footerRange.Font.ColorIndex = WdColorIndex.wdBlack;
                    footerRange.Font.Size = 20;
                    footerRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    footerRange.Text = DateTime.Today.ToString("dd.MM.yyyy") + "            " + "Подпись___________";
                }

                object filename = $"work_out.docx";
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
    }
}
