using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aspose.Cells;


namespace TrackSpoolgen
{
    public partial class Form1 : Form
    {        
        public Form1()
        {
            InitializeComponent();
        }
        //模板文件相对路径
        //string Template_File_Path = @"..\..\Template\Template.xlsx";
        string Template_File_Path = @"Template\Template.xlsx";//模板相对路径
        string strDesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);//桌面路径
        int i = 1;

        private void read_Click(object sender, EventArgs e)
        {            
            //  打开 Excel 模板
            Workbook TemplateWorkbook = File.Exists(Template_File_Path) ? new Workbook(Template_File_Path) : new Workbook();
            //  打开模板第一个sheet
            Worksheet TemplateSheet = TemplateWorkbook.Worksheets[0];
            Cells cells0 = TemplateSheet.Cells;

            FolderBrowserDialog dialog = new FolderBrowserDialog();//打开文件夹对话框
            if(dialog.ShowDialog()==DialogResult.OK)
            {
                string FolderPath= dialog.SelectedPath;
                textBoxPath.Text = FolderPath;
                
                DirectoryInfo root = new DirectoryInfo(FolderPath);
                FileInfo[] files1 = root.GetFiles();
                
                int fileCount = 0;
                foreach (FileInfo file in files1)
                {
                    string fileName = file.Name;
                    string fileFullName = file.FullName;
                   
                    if (fileName.Contains("Material"))
                    {
                        fileCount++;
                        rtb.AppendText(fileName+"\n");//将文件名添加至textbox中   
                        
                        HandleMaterial(fileFullName, cells0);                      
                      
                    }                  
                    txtCount.Text = "共读取到"+" "+fileCount.ToString()+ " " + "个文件";
                }
               
                TemplateWorkbook.Save(strDesktopPath+@"\TrackingList.xlsx");                
                MessageBox.Show("TrackingList： 已完成", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }
        
        private void HandleMaterial(string fileFullName,Cells cells0)
        {
            string line = string.Empty;
            StreamReader reader = new StreamReader(fileFullName, Encoding.Default);
            //int i = 1;
            
            while ((line = reader.ReadLine()) != null)
            {
                
                string col1 = line.Substring(0, 10).Trim();
                if(col1=="SPCCLASS")
                {
                    continue;
                }
                string col2 = line.Substring(10, 50).Trim();
                string col3 = line.Substring(60, 30).Trim();
                string col4 = line.Substring(90, 30).Trim();
                string col5 = line.Substring(120, 30).Trim();
                string col6 = line.Substring(150, 30).Trim();
                string col7 = line.Substring(180, 30).Trim();
                string col8 = line.Substring(210, 30).Trim();
                string col9 = line.Substring(240, 30).Trim();
                string col10 = line.Substring(270, 30).Trim();
                string col11 = line.Substring(300, 31).Trim();
                string col12 = line.Substring(331, 29).Trim();
                string col13 = line.Substring(360, 30).Trim();
                string col14 = line.Substring(390, 29).Trim();
                string col15 = line.Substring(419, 31).Trim();
                string col16 = line.Substring(450, 30).Trim();
                string col17 = line.Substring(480, 30).Trim();
                string col18 = line.Substring(510, 30).Trim();
                string col19 = line.Substring(540, 30).Trim();
                string col20 = line.Substring(570, 30).Trim();
                string col21 = line.Substring(600, 31).Trim();
                string col22 = line.Substring(630, 29).Trim();
                string col23 = line.Substring(660, 30).Trim();
                string col24 = line.Substring(690, 29).Trim();
                string col25 = line.Substring(720, 30).Trim();
                string col26 = line.Substring(750, 30).Trim();
                string col27 = line.Substring(780, 30).Trim();
                string col28 = line.Substring(810, 140).Trim();
                string col29 = line.Substring(950, line.Length-950).Trim();
                
                //cell和表格的对应关系是-1的关系
                cells0[i, 15 - 1].PutValue(col2);
                cells0[i, 16 - 1].PutValue(col4);
                cells0[i, 23 - 1].PutValue(col6);
                cells0[i, 22 - 1].PutValue(col6);
                if (col9.Contains("MM"))
                {
                    string[] unitArry9 = col9.Split(' ');
                    cells0[i, 39 - 1].PutValue(unitArry9[1]);
                    cells0[i, 40 - 1].PutValue(unitArry9[0]);
                    cells0[i, 42 - 1].PutValue(unitArry9[0]);
                }                  
                else                   
                {
                    cells0[i, 38 - 1].PutValue(col9);
                    cells0[i, 39 - 1].PutValue("Pcs");//单位
                }
                //string[] arry=cells[i, 14 - 1].Value.ToString().Split('x');//待定x的形式，x不存在问题
                //cells0[i, 28 - 1].PutValue(arry[0]);
                cells0[i, 28 - 1].PutValue(col14);
                //cells0[i, 29 - 1].PutValue(arry[1]);
                cells0[i, 27 - 1].PutValue(col15);
                cells0[i, 57 - 1].PutValue(col16);
                cells0[i, 10 - 1].PutValue(col19);
                cells0[i, 18 - 1].PutValue(col25);
                cells0[i, 17 - 1].PutValue(col26);
                cells0[i, 32 - 1].PutValue(col27);
                cells0[i, 52 - 1].PutValue(col28);
                cells0[i, 69 - 1].PutValue(col29);
                i++;

                pgb.Minimum = 0;//进度条
                pgb.Maximum = i - 1;//最大值
                pgb.Step = 1;
                pgb.PerformStep();// 使用performstep方法按Step值递增
                
            }          

        }
        private void btnCheck_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(strDesktopPath + @"\TrackingList.xlsx");//打开桌面路径下的TrackingList

        }           
    }
}
