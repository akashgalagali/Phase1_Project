using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConsoleApp1
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    namespace ConsoleApp2.phase1
    {
        class TeacherInformation
        {
            public int Id { get; set; }
            public string TName { get; set; }
            public int Class { get; set; }
            public string Section { get; set; }
            public TeacherInformation()
            {

            }
            public TeacherInformation(int id, string name, int Class, string section)
            {
                this.Id = id;
                this.TName = name;
                this.Class = Class;
                this.Section = section;

            }
        }

        class TeacherBO
        {

            public List<TeacherInformation> teachers { get; set; }
            public TeacherBO()
            {
                teachers = new List<TeacherInformation>();
            }
            
            public void GetAllTeachers(string path)
            {
               
                StreamReader sr = new StreamReader(path);
                Console.WriteLine("ID,Name,Class,Section");
                while (sr.Peek()>= 0)
                {
                    string line = sr.ReadLine();
                    List<string> data = line.Split(",", 4).ToList();
                    teachers.Add(new TeacherInformation(Convert.ToInt32(data[0]), data[1], Convert.ToInt32(data[2]), data[3]));
                    Console.WriteLine(line);
                }
                sr.Close();
                
            }
            public void AddTeacher(TeacherInformation t, string path)
            {
                List<string> final = new List<string>();
                StreamReader sr = new StreamReader(path);
                bool flag = false;
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    List<string> data = line.Split(",", 4).ToList();
                    if (data[0] == t.Id.ToString())
                    {
                        flag = true;
                        Console.WriteLine("Entered Teacher Id already Exists, please give unique ID");
                    }

                }
                sr.Close();
                if (flag == false)
                {
                    StreamWriter sw = new StreamWriter(path, append: true);
                    sw.WriteLine(t.Id + "," + t.TName + "," + t.Class + "," + t.Section);
                    sw.Close();
                    Console.WriteLine("Record Added successfully !!");
                }
            }
            public void UpdateTeacher(TeacherInformation t, string path)
            {
                List<string> final = new List<string>();
                StreamReader sr = new StreamReader(path);
                bool flag=false;
                while (sr.Peek()>= 0)
                {
                    string line = sr.ReadLine();
                    List<string> data = line.Split(",", 4).ToList();
                    string dataLine;
                    if(data[0] == t.Id.ToString())
                    {
                        flag = true;
                        data[1] = t.TName;
                        data[2] = t.Class.ToString();
                        data[3] = t.Section;
                        dataLine = string.Join(",",data);
                        final.Add(dataLine);
                    }
                    else
                    {
                        dataLine = string.Join(",", data);
                        final.Add(dataLine);
                    }

                }
                sr.Close();
                string fileContent=string.Join("\n",final);
                StreamWriter swUpdate = new StreamWriter(path);
                swUpdate.WriteLine(fileContent);
                swUpdate.Close();
                if(flag==false)
                    Console.WriteLine($"The Requested Teacher record with Id:{t.Id} does not exists");
                else
                {
                    Console.WriteLine("Record updates successfully !!");
                }
            }
            public void TeacherDetailsById(int id, string path)
            {
                bool found = false;
                StreamReader sr = new StreamReader(path);
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    List<string> data = line.Split(",", 4).ToList();
                    if (data[0] == id.ToString())
                    {
                        found = true;
                        Console.WriteLine(line);
                        break;
                    }
                }
                if (found==false)
                    Console.WriteLine($"no Teacher record found with id: {id}");
                sr.Close();
            }
            public void DeleteTeacher(int id, string path)
            {
                List<string> final = new List<string>();
                StreamReader sr = new StreamReader(path);
                int flag = 0;
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    List<string> data = line.Split(",", 4).ToList();
                    string ss;
                    if (data[0] == id.ToString())
                    {
                        flag = 1;
                        Console.WriteLine("Record found and Deleted Successfully");
                    }
                    else
                    {
                        ss = string.Join(",", data);
                        final.Add(ss);
                    }

                }
                sr.Close();
                string s3 = string.Join("\n", final);
                StreamWriter sw3 = new StreamWriter(path);
                sw3.WriteLine(s3);
                sw3.Close();
                if(flag==0)
                    Console.WriteLine($"The Requested Teacher Record with Id:{id} Does not exists");
            }

    }


        class TeacherDB
        {
            static void Main(string[] args)
            {
                TeacherBO teacherObj = new TeacherBO();
                string path = @"C:\Users\11035923\Documents\Teacher.txt";
                
                while (true) { 
                Console.WriteLine("Please Enter the number corresponding to the Operation you want to Perform\n 1 : Get All Teachers \n 2 : Add Teacher \n 3 : Update Teacher \n 4 : Delete Teacher \n 5 : Get Teacher By Id \n 6 : Close App or Exit");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                            //Display Teacher Details
                            teacherObj.GetAllTeachers(path);
                            break;
                    case 2:
                            //add teacher
                            TeacherInformation tr1 = new TeacherInformation();
                            Console.WriteLine(" enter ID: ");
                            int Id = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine(" enter class:");
                            int Class = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("enter section:");
                            string Sec = Console.ReadLine();
                            Console.WriteLine(" enter name: ");
                            string TName = Console.ReadLine();
                            tr1 = new TeacherInformation(Id, TName, Class, Sec);
                            teacherObj.AddTeacher(tr1, path);
                            break;
                    case 3:
                            //Update record
                            TeacherInformation tru = new TeacherInformation();
                            Console.WriteLine(" enter ID: ");
                            int Idu = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine(" enter class:");
                            int Classu = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("enter section:");
                            string Secu = Console.ReadLine();
                            Console.WriteLine(" enter name: ");
                            string TNameu = Console.ReadLine();
                            tru = new TeacherInformation(Idu, TNameu, Classu, Secu);
                            teacherObj.UpdateTeacher(tru, path);
                            break;
                    case 4:
                            //delete record
                            Console.WriteLine(" enter ID: ");
                            int Idd = Convert.ToInt32(Console.ReadLine());
                            teacherObj.DeleteTeacher(Idd, path);
                            break;
                    case 5:
                            Console.WriteLine("Enter the Id");
                            int teacherId = int.Parse(Console.ReadLine());
                            teacherObj.TeacherDetailsById(teacherId, path);
                            break;
                        
                    case 6:
                            goto exit;
                }
                    Console.WriteLine("==========================================================================================");
                }
                exit:
                Console.WriteLine("Thank you! the Process has been completed");
            }
        }
    }
}