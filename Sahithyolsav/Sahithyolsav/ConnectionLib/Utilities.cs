using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Net;
using System.Collections;
using System.ComponentModel;
using System.Resources;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;
using System.Configuration;
using System.IO.Compression;
using System.Drawing;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;

using System.Data.OleDb;
using System.IO.MemoryMappedFiles;
using System.Management;


namespace ConnectionLib
{
    public class Utilities
    {
        public static SqlConnection Connection;
        public static SqlCommand Command;
        private static string EncryptionKey = "!#853g`de";
        private static byte[] key = { };
        private static byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        public static DateTime LicenseExpiryDate;

        public enum DataBase
        {
            Sahithyolsav
        };

        public static String GetConnectionString(DataBase DBName)
        {
            String ConStr;
            if (DBName == DataBase.Sahithyolsav)
            {
                return ConStr = ConfigurationManager.ConnectionStrings["DBConString"].ConnectionString;
            }
            else
            {
                return ConStr = "";
            }
        }


        public static SqlConnection createConnection(DataBase DBName)
        {
            String ConStr;
            if (DBName == DataBase.Sahithyolsav)
            {
                ConStr = GetConnectionString(DataBase.Sahithyolsav);
            }
            else
            {
                ConStr = "";
            }
            try
            {
                Connection = new System.Data.SqlClient.SqlConnection();
                Connection.ConnectionString = ConStr;
                Connection.Open();
                return Connection;

            }
            catch
            {
                return null;
            }
        }


        public static DateTime getToday()
        {
            //String Query;
            //DataSet ds = new DataSet();
            //Query = "SELECT GETDATE() as todayDate ";
            DateTime timeUtc = System.DateTime.UtcNow;
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
            try
            {
                return cstTime;
            }
            catch
            {
                return DateTime.Now;
            }

            finally
            {
                //ds.Dispose();
                //Query = "";
            }

        }


        public static string Encrypt(string Input)
        {
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(EncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] inputByteArray = Encoding.UTF8.GetBytes(Input.Replace("+", " "));
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string Decrypt(string Input)
        {
            Byte[] inputByteArray = new Byte[Input.Length];

            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(EncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(Input.Replace(" ", "+"));
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                Encoding encoding = Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static bool SendMail(String MailToAddr, String MailSubject, String MailContent)
        {
            try
            {
                MailMessage Msg = new MailMessage();
                // Sender e-mail address.
                String MailFrom = "sunilb@Callystro.com";
                Msg.From = new MailAddress(MailFrom);
                // Recipient e-mail address.
                Msg.To.Add(MailToAddr);
                Msg.Subject = MailSubject;
                // File Upload path
                string mailbody = MailContent;
                //   LinkedResource myimage = new LinkedResource(FileName);
                // Create HTML view
                AlternateView htmlMail = AlternateView.CreateAlternateViewFromString(mailbody, null, "text/html");
                // Set ContentId property. Value of ContentId property must be the same as
                // the src attribute of image tag in email body. 
                //myimage.ContentId = "companylogo";
                //htmlMail.LinkedResources.Add(myimage);
                Msg.AlternateViews.Add(htmlMail);
                // your remote SMTP server IP.
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("sunilb@Callystro.com", "149162536");
                smtp.EnableSsl = true;
                smtp.Send(Msg);
                Msg = null;
                return true;
            }
            catch
            {
                return false;

            }
            finally
            {

            }

        }
        public static bool ValidEmailFormat(string Email) //Server side code to validate valid email format.
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(Email))
                return (true);
            else
                return (false);
        }
        public static void ExportGrid(GridView gv, String ExportType, String Filename)
        {
            if (ExportType == "pdf")
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + Filename + ".pdf");

                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);


                gv.AllowPaging = false;
                gv.HeaderRow.ForeColor = System.Drawing.Color.Black;
                gv.FooterRow.ForeColor = System.Drawing.Color.Black;

                gv.HeaderRow.Style.Add("font-Color", "Black");
                gv.HeaderRow.Style.Add("font-size", "13px");
                gv.HeaderRow.Style.Add("text-decoration", "none");
                gv.HeaderRow.Style.Add("font-family", "Arial, Helvetica, sans-serif;");

                gv.Style.Add("font-Color", "Black");
                gv.Style.Add("text-decoration", "none");
                gv.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                gv.Style.Add("font-size", "11px");
                gv.ForeColor = System.Drawing.Color.Black;

                gv.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);

                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                htmlparser.Parse(sr);
                pdfDoc.Close();
                HttpContext.Current.Response.Write(pdfDoc);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else if (ExportType == "word")
            {

                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", Filename + ".doc"));
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.ContentType = "application/ms-word";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                gv.HeaderRow.ForeColor = System.Drawing.Color.Black;
                gv.HeaderRow.Style.Add("font-Color", "Black");
                gv.HeaderRow.Style.Add("font-size", "17px");
                gv.HeaderRow.Style.Add("text-decoration", "none");
                gv.HeaderRow.Style.Add("font-family", "Arial, Helvetica, sans-serif;");

                gv.Style.Add("font-Color", "Black");
                gv.Style.Add("text-decoration", "none");
                gv.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                gv.Style.Add("font-size", "15px");
                gv.ForeColor = System.Drawing.Color.Black;

                gv.RenderControl(htw);
                HttpContext.Current.Response.Write(sw.ToString());
                HttpContext.Current.Response.End();
            }

            else if (ExportType == "excel")
            {
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", Filename + ".xls"));
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                for (int i = 0; i < gv.HeaderRow.Cells.Count; i++)
                {
                    gv.HeaderRow.Cells[i].Style.Add("background-color", "#507CD1");
                }
                int j = 1;

                //This loop is used to apply style to cells based on particular row
                foreach (GridViewRow gvrow in gv.Rows)
                {
                    //gvrow.BackColor = Color.WHITE;
                    if (j <= gv.Rows.Count)
                    {
                        if (j % 2 != 0)
                        {
                            for (int k = 0; k < gvrow.Cells.Count; k++)
                            {
                                gvrow.Cells[k].Style.Add("background-color", "#EFF3FB");
                            }
                        }
                    }
                    j++;
                }
                gv.RenderControl(htw);
                HttpContext.Current.Response.Write(sw.ToString());
                HttpContext.Current.Response.End();
            }
        }

        //public static void ExportChart(Chart Chart, String Filename)
        //{
        //    Document pdfDoc = new Document(PageSize.A4, 30f, 30f, 50f, 50f);
        //    PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
        //    pdfDoc.Open();
        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        Chart.SaveImage(stream, ChartImageFormat.Png);
        //        iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(stream.GetBuffer());
        //        chartImage.ScalePercent(60f);
        //        pdfDoc.Add(chartImage);

        //        pdfDoc.Close();

        //        HttpContext.Current.Response.ContentType = "application/pdf";
        //        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + Filename + ".pdf");
        //        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //        HttpContext.Current.Response.Write(pdfDoc);
        //        HttpContext.Current.Response.End();
        //    }
        //}

        public static void GridNoResultFound(GridView gv, DataView dv)
        {
            DataRowView NewRow = dv.AddNew();
            gv.DataSource = dv;
            gv.DataBind();
            int columncount = gv.Rows[0].Cells.Count;
            gv.Rows[0].Cells.Clear();
            gv.Rows[0].Cells.Add(new TableCell());
            gv.Rows[0].Cells[0].ColumnSpan = columncount;
            gv.Rows[0].Cells[0].Text = "No Records Found";
            gv.Rows[0].Cells[0].Font.Bold = true;
            gv.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
            gv.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
        }


        public static bool ValidFileFormat(string ext) //Server side code to validate Image format.
        {
            string[] validFileTypes = { "BMP", "GIF", "PNG", "JPG", "JPEG" };
            bool isValidFile = false;

            for (int i = 0; i < validFileTypes.Length; i++)
            {
                if (ext == "." + validFileTypes[i])
                {
                    isValidFile = true;
                    break;
                }
            }
            if (isValidFile)
                return (true);
            else
                return (false);
        }
        public static bool ValidVoiceFormat(string ext) //Server side code to validate audio format.
        {
            string[] validFileTypes = { "mp3" };
            bool isValidFile = false;

            for (int i = 0; i < validFileTypes.Length; i++)
            {
                if (ext == "." + validFileTypes[i])
                {
                    isValidFile = true;
                    break;
                }
            }
            if (isValidFile)
                return (true);
            else
                return (false);
        }
        public static bool ValidFileFormat2(string ext) //Server side code to validate excel format.
        {
            string[] validFileTypes = { "XLSX", "XLS", "CSV" };
            bool isValidFile = false;

            for (int i = 0; i < validFileTypes.Length; i++)
            {
                if (ext == "." + validFileTypes[i])
                {
                    isValidFile = true;
                    break;
                }
            }
            if (isValidFile)
                return (true);
            else
                return (false);
        }
        public static bool ValidCSV(string ext) //Server side code to validate excel format.
        {
            string[] validFileTypes = { "CSV" };
            bool isValidFile = false;

            for (int i = 0; i < validFileTypes.Length; i++)
            {
                if (ext == "." + validFileTypes[i])
                {
                    isValidFile = true;
                    break;
                }
            }
            if (isValidFile)
                return (true);
            else
                return (false);
        }

    }
}
