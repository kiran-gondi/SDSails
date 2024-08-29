namespace SDSailsCommon.Utility
{
    using System;
    using System.Data;
    using System.Web;
    using System.Collections.Generic;
    using System.Text;
    using System.IO;
    using System.Collections.Specialized;
    using System.Xml.Serialization;
    using System.Xml;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Configuration;
    using System.Globalization;

    public static class ServerLog
    {
        static public void LogIt(string sTitle, string sData)
        {
            string strServerLogPath = ConfigurationManager.AppSettings["ServerLogPath"];
            string sLogFileName = string.Empty;
            if (!string.IsNullOrEmpty(strServerLogPath))
            {
                // write to file specified in web.config
                sLogFileName = strServerLogPath + "\\ServerLog." + DateTime.Now.ToString("yyyy.MM.dd", CultureInfo.InvariantCulture) + ".htm";
            }
            else
            {
                // write to current folder
                sLogFileName = System.AppDomain.CurrentDomain.BaseDirectory + "\\ServerLog." + DateTime.Now.ToString("yyyy.MM.dd", CultureInfo.InvariantCulture) + ".htm";
            }

            LogIt(sLogFileName, sTitle, sData);
        }

        static public void LogIt(string sFullFileName, string sTitle, string sData)
        {
            if (string.IsNullOrEmpty(sFullFileName))
            {
                throw new Exception("Invalid parameter passed to ServerLogId");
            }

            string sHtml = "";

            sHtml += "<hr noshade color=#000000 size=1>";
            sHtml += " " + DateTime.Now.ToString();
            sHtml += " " + sTitle;
            sHtml += " " + sData;

            try
            {
                System.IO.StreamWriter oStreamWriter = new System.IO.StreamWriter(sFullFileName, true);
                oStreamWriter.WriteLine(sHtml);
                oStreamWriter.Close();
            }
            catch (Exception ex)
            {
                LogIt("Failure", DisplayInTextArea(DisplayInTextArea(ObjectToXMLString(ex.Message))));
                throw new Exception("Failure");
            }
        }

        static public void AppErrorLogIt(string sTitle, string sData)
        {

            string strServerLogPath = ConfigurationManager.AppSettings["ServerLogPath"];
            string sLogFileName = string.Empty;
            if (!string.IsNullOrEmpty(strServerLogPath))
            {
                // write to file specified in web.config
                sLogFileName = strServerLogPath + "\\ServerLog." + DateTime.Now.ToString("yyyy.MM.dd", CultureInfo.InvariantCulture) + ".htm";
            }
            else
            {
                // write to current folder
                sLogFileName = System.AppDomain.CurrentDomain.BaseDirectory + "\\ServerLog." + DateTime.Now.ToString("yyyy.MM.dd", CultureInfo.InvariantCulture) + ".htm";
            }

            AppErrorLogIt(sLogFileName, sTitle, sData);
        }

        static public void AppErrorLogIt(string sFullFileName, string sTitle, string sData)
        {
            if (string.IsNullOrEmpty(sFullFileName))
            {
                throw new Exception("Invalid parameter passed to ServerLogId");
            }

            string sHtml = "";

            sHtml += "<hr noshade color=#000000 size=1>";
            sHtml += " " + DateTime.Now.ToString();
            sHtml += " " + sTitle;
            sHtml += " " + sData;

            try
            {
                System.IO.StreamWriter oStreamWriter = new System.IO.StreamWriter(sFullFileName, true);
                oStreamWriter.WriteLine(sHtml);
                oStreamWriter.Close();
            }
            catch (Exception ex)
            {
                LogIt("Failure", DisplayInTextArea(DisplayInTextArea(ObjectToXMLString(ex.Message))));
                throw new Exception("Failure");
            }
        }

        static public string FormatRed(string pHtml)
        {
            return "<font color='FF0000'><b>" + pHtml + "</b></font>";
        }

        static public string FormatGreen(string pHtml)
        {
            return "<font color='008000'><b>" + pHtml + "</b></font>";
        }

        static public string DisplayInTextArea(string sHTML)
        {
            string sReturn = "<br><textarea rows='10' cols='75'>" + sHTML + "</textarea>";
            return sReturn;
        }

        static public string ObjectToXMLString(Object oObject)
        {
            try
            {
                string sXmlizedString = null;
                MemoryStream oMemoryStream = new MemoryStream();
                XmlSerializer oXS = new XmlSerializer(oObject.GetType());
                XmlTextWriter xmlTextWriter = new XmlTextWriter(oMemoryStream, System.Text.Encoding.UTF8);
                oXS.Serialize(xmlTextWriter, oObject);
                oMemoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                sXmlizedString = UTF8ByteArrayToString(oMemoryStream.ToArray());

                return MakePrettyXML(sXmlizedString);

            }
            catch (Exception ex)
            {
                LogIt("Failure", DisplayInTextArea(DisplayInTextArea(ObjectToXMLString(ex.Message))));
                throw new Exception("Failure");
            }
        }

        static public string NameValueToString(NameValueCollection oNameValueCollection)
        {
            string sReturn = "";
            foreach (string sKey in oNameValueCollection)
            {
                sReturn += sKey + "=" + oNameValueCollection[sKey] + Environment.NewLine;
            }
            return sReturn;
        }

        static private string UTF8ByteArrayToString(byte[] characters)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            string constructedString = encoding.GetString(characters);
            return constructedString;
        }

        static public MemoryStream StringToMemoryStream(string value)
        {
            byte[] byteArray = new byte[value.Length];
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byteArray = encoding.GetBytes(value);
            MemoryStream memoryStream = new MemoryStream(byteArray);
            return memoryStream;
        }

        static public string MakePrettyXML(string pXMLString)
        {
            const string REMOVENAMESPACE = " xmlns=\"http://www.citysprint.co.uk/MOD\"";
            pXMLString = pXMLString.Replace(REMOVENAMESPACE, "").Replace("><", ">\n<");
            return pXMLString;
        }
    }
}
