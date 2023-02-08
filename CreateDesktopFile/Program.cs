// See https://aka.ms/new-console-template for more information

using System.Text;

string username = Environment.UserName;
string saveLocation = $"{Directory.GetDirectoryRoot(Directory.GetCurrentDirectory())}home/{username}/DesktopFiles/";
string postfix = ".desktop";
string name = String.Empty;
string executablePath = String.Empty;
string comment = String.Empty;
string type = String.Empty;
string categories = String.Empty;
string terminalApp = String.Empty;
string fullPath = String.Empty;

Console.WriteLine(saveLocation);

Console.WriteLine("Name of Desktop File and Application:");
name = Console.ReadLine();

Console.WriteLine("Executable Path of Program (inc prefixed commands):");
executablePath = Console.ReadLine();

Console.WriteLine("Comments:");
comment = Console.ReadLine();

Console.WriteLine("Type (ex: Application):"); 
type = Console.ReadLine();   

Console.WriteLine("Categories (seperate with ; ):"); 
categories = Console.ReadLine();   

Console.WriteLine("Terminal App? (true / false):"); 
terminalApp = Console.ReadLine();

CreateFile();

void CreateFile()
{
    string compiledName = name + postfix;
    fullPath = saveLocation + compiledName;
    Console.WriteLine(fullPath);
    try
    {
        if (File.Exists(fullPath))
        {
            name += "1";
            compiledName = name + postfix;
            fullPath = saveLocation + compiledName;
        }

        using (FileStream fs = File.Create(fullPath))
        {
            Byte[] body = new UTF8Encoding(true).GetBytes(
                "[Desktop Entry] \n" +
                $"Name={name}\n" +
                $"Comment={comment}\n" +
                $"Exec={executablePath}\n" +
                $"Terminal={terminalApp}\n" +
                $"Type={type}\n" +
                $"Categories={categories}\n"
            );
            fs.Write(body, 0, body.Length);
        }

        using (StreamReader sr = File.OpenText(saveLocation + compiledName))
        {
            string s = String.Empty;
            while ((s = sr.ReadLine()) != null)
            {
                Console.WriteLine(s);
            }
        }
    }
    catch (DirectoryNotFoundException Ex)
    {
        Console.WriteLine(Ex.ToString());
    
    }
    catch (Exception Ex)
    {
        Console.WriteLine(Ex.ToString());   
    }
}


