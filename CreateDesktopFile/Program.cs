using System.Text;

/*
 * Really simple console app which will ask you:
 * Name of App
 * Executable path of program
 * Comments on what the program is
 * What type of console app this is (Ex: Application)
 * What categories this app should be under in Start Menu (delimeter is a semicolon ; )
 * Does this app run in the Terminal?
 *
 * It outputs the generated Desktop Files in /home/<user>/DesktopFiles
 * If this folder does not exist, it will create it automatically.
 *
 *
 * 
 * Additional functionality which could be added:
 * 1. When running the terminal app through terminal, add in a output path (as an argument)
 * instead of using the default DesktopFiles.
 */

string username = Environment.UserName;
string homeFolder = $"{Directory.GetDirectoryRoot(Directory.GetCurrentDirectory())}home/{username}";
string saveLocation = $"{homeFolder}/DesktopFiles/";
string fullPath = String.Empty;
string postfix = ".desktop";

//Below are the variables filled out by the user
string name = String.Empty;
string executablePath = String.Empty;
string comment = String.Empty;
string type = String.Empty;
string categories = String.Empty;
string terminalApp = String.Empty;
string iconPath = String.Empty;

GetInfoFromUser();

CreateFile();

#region Main
void GetInfoFromUser()
{
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
    
    Console.WriteLine("Icon Path:"); 
    iconPath = Console.ReadLine();
}



void CreateFile()
{
    if (!CheckIfFolderExists(saveLocation))
    {
        Console.WriteLine("Creating Folder now...");
        CreateFolder(saveLocation);
    }

    string compiledName = name + postfix;
    fullPath = saveLocation + compiledName;
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
                $"Categories={categories}\n" +
                $"Icon={iconPath}\n"
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

#region Utility

bool CheckIfFolderExists(string folderLocation)
{
    if (Directory.Exists(folderLocation))
    {
        Console.WriteLine("Folder exists");
        return true;
    }
    else
    {
        Console.WriteLine("Folder does not exist");
        return false;
    }
}

void CreateFolder(string folderLocation)
{
    Directory.CreateDirectory(folderLocation);
}

#endregion

#endregion

