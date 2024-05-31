using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;



namespace FHTW.Swen2.Places.Model
{
    /// <summary>This class provides report generation methods.</summary>
    public static class Reporting
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // public static methods                                                                                    //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>Creates a place report.</summary>
        /// <param name="place">Place.</param>
        /// <param name="file">File name.</param>
        public static void GeneratePlaceReport(Place place, string file)
        {
            PdfWriter wr = new(file);
            PdfDocument pdf = new(wr);

            Document doc = new(pdf);

            doc.Add(new Paragraph(place.Name)
                        .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                        .SetFontSize(14).SetBold());

            doc.Add(new Paragraph(place.Description)
                        .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                        .SetFontSize(10));

            foreach(Story i in place.Stories)
            {
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph(i.Text)
                        .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                        .SetFontSize(10));

                foreach(string k in i.Pictures)
                {
                    try
                    {
                        doc.Add(new Image(ImageDataFactory.Create(Root.Config.ImagePath.TrimEnd('\\') + '\\' + k)).ScaleToFit(300, 200));
                    } catch(Exception) {}
                }
            }

            doc.Close();
            pdf.Close();
            wr.Close();
        }
    }
}
