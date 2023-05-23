using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BDayWishMail.Models
{
    class Util
    {
        public static Hashtable GROUP_HASHTABLE = new Hashtable();
        public static Hashtable TEMPLATE_HASHTABLE = new Hashtable();

        [DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileStringW",
        SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention =
        CallingConvention.StdCall)]

        private static extern int GetPrivateProfileString(string lpAppName,
        string lpKeyName, string lpDefault, StringBuilder lpReturnString, int nSize, string lpFilename);
        static String filePath = "..\\App_Data\\Config.ini";


        public static String getIniString(String section, String key)
        {
            StringBuilder ReturnString = new StringBuilder(255);
            int x = GetPrivateProfileString(section, key, "", ReturnString, 255, filePath);
            return ReturnString.ToString().Trim();
        }


        public static string getIniConfigString(string psSection, string psKey)
        {
            return getIniString(psSection, psKey);
        }

        public static String isEntryMode()
        {
            return getIniString("MODE", "ENTRY");
        }

        public static String isFireMode()
        {
            return getIniString("MODE", "FIRE");
        }

        public static String GetDbString()
        {
            String dbString = "";
            String SERVER = getIniString("Main", "SERVER").Trim();
            String dataSource = getIniString("Main", "DataSource").Trim();
            String userid = getIniString("Main", "DBUser").Trim();
            String passwd = getIniString("Main", "DBPassword").Trim();

            dbString = "server = " + SERVER + "; uid = " + userid + "; pwd = " + passwd + "; database = " + dataSource + "; respect binary flags = false";
            return dbString;

        }

        public static String GetDbStringBackUp()
        {
            String dbString = "";
            String provider = getIniString("BackUp", "DBProvider").Trim();
            String dataSource = getIniString("BackUp", "DataSource").Trim();
            String userid = getIniString("BackUp", "DBUser").Trim();
            String passwd = getIniString("BackUp", "DBPassword").Trim();
            dbString = "Provider=" + provider + ";DSN=" + dataSource + ";User ID=" + userid + ";Pwd=" + passwd + ";Unicode=True;Persist Security Info=True;";
            return dbString;

        }

        public static String GetHostIpAddress(String machName)
        {
            return getIniString("HOST", machName);
        }
        /*
         * market start and end time
         */
        public static String GetScenePath(String sceneValue)
        {
            return getIniString("SCENE", sceneValue);
        }

        public static String GetSQLQuery(string Sql)
        {
            return getIniString("QUERY", Sql);
        }

        public static String GetCcstring(String ccmailid)
        {
            return getIniString("CC", ccmailid);
        }



    }
}
