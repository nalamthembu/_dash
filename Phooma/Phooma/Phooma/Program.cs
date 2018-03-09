using System;
using System.IO;
using Microsoft.Win32;

namespace Phooma
{
	class MainClass
	{

		public static void Main (string[] args)
		{

			Console.Title = "Phooma";
			Console.Write ("Phooma>");
			String input = Console.ReadLine ();
			ReadCommand (input);
		}

		public static void ReadCommand(String c)
		{
			if (c.Contains ("diskInfo")) {
				Console.Clear ();
				System.IO.DriveInfo di = new System.IO.DriveInfo (@"C:\");
				Console.WriteLine ("Total Free Space : " + di.TotalFreeSpace + " bytes");
				Console.WriteLine ("Hard Drive Label : " + di.VolumeLabel);
				System.IO.DirectoryInfo dirInfo = di.RootDirectory;
				Console.WriteLine (dirInfo.Attributes.ToString ());
				System.IO.FileInfo[] fileNames = dirInfo.GetFiles ("*.*");

				Console.WriteLine ("\nFile List :  \n");
				foreach (System.IO.FileInfo fi in fileNames) {
					Console.WriteLine ("{0}: {1}: {2}", fi.Name, fi.LastAccessTime, fi.Length);
				}

				String[] arguments = { "Args", "test" };
				Main (arguments);

			} else if (c.Contains ("appDir")) {
				string currentDirName = System.IO.Directory.GetCurrentDirectory ();
				Console.WriteLine (currentDirName);
				String[] arguments = { "Args", "test" };
				Main (arguments);
			} else if (c.Contains ("getFiles") || c.Contains ("getFiles") || c.Contains ("GETFILES") || c.Contains ("getfiles")) {
				
				Console.WriteLine ("Please Type a Directory");
				string currentDirName = Console.ReadLine ();

				Console.WriteLine ("What type of file should I look for?");
				
				string fileType = Console.ReadLine ();

				Console.Clear ();

				Console.WriteLine ("Heres what was found: \n ===================");
				
				string[] files = System.IO.Directory.GetFiles (currentDirName, "*." + fileType);
				
				foreach (string s in files) {
					// Create the FileInfo object only when needed to ensure
					// the information is as current as possible.
					System.IO.FileInfo fi = null;
					try {
						fi = new System.IO.FileInfo (s);
					} catch (System.IO.FileNotFoundException e) {
						// To inform the user and continue is
						// sufficient for this demonstration.
						// Your application may require different behavior.
						Console.WriteLine (e.Message);
						continue;
					}
					Console.WriteLine ("{0} : {1}", fi.Name, fi.Directory);
				}
				String[] args = { "args", "test" };
				Main (args);

				if (!System.IO.Directory.Exists (currentDirName)) {
					Console.WriteLine (currentDirName + " does not exist");
				}
			} else 
				//Create
				if (c.Contains ("create") || c.Contains("crte")) {	
						
				Console.Clear ();
				Console.WriteLine ("CREATE\n===========");
				//NXT
				
				// Specify a name for your top-level folder.
				Console.WriteLine ("Specify a name for your top-level folder. (PATH)");
				string folderName = Console.ReadLine ();
				// To create a string that specifies the path to a subfolder under your
				// top-level folder, add a name for the subfolder to folderName.
				Console.WriteLine ("Specify a name for your subfolder.");
				string subFolder = Console.ReadLine ();
				string pathString = System.IO.Path.Combine (folderName, subFolder);
				//Create Dir
				System.IO.Directory.CreateDirectory (pathString);
				string fileName = System.IO.Path.GetRandomFileName ();


				//Combine again
				pathString = System.IO.Path.Combine (pathString, fileName);

				Console.WriteLine ("Verifying \n");
				Console.WriteLine ("Path to my file: {0}\n", pathString);
				Console.WriteLine ("Loading");

				if (!System.IO.File.Exists (pathString)) {
					using (System.IO.FileStream fs = System.IO.File.Create (pathString)) {
						for (int i = 5; i > 0; i--) {
							Console.CursorSize = i;
						}
						for (byte i = 0; i < 100; i++) {
							fs.WriteByte (i);
							Console.ForegroundColor = ConsoleColor.Green;
							Console.Write ("▒");
						}
						Console.WriteLine ("\n");
						Console.ForegroundColor = ConsoleColor.Gray;
					}
				}
				Console.WriteLine ("Reading...\n");
				try {
					byte[] readBuffer = System.IO.File.ReadAllBytes (pathString);
					foreach (byte b in readBuffer) {
						Console.Write (b + " ");
					}
					Console.WriteLine ();
				} catch (System.IO.IOException e) {
					Console.WriteLine (e.Message);
					
				}
				Console.ReadLine ();
				String[] args = { "args", "test" };
				Main (args);
			} else if (c.Contains ("Move")) {
				String[] args = { "args", "test" };
				Console.Clear ();
				Console.WriteLine ("Source Files");
				string sourceFile = @Console.ReadLine ();
				Console.Clear ();
				Console.WriteLine ("Destination Files (EXPECTED RESULT");
				string destinationFile = @Console.ReadLine ();
				// To move a file or folder to a new location:
				System.IO.File.Move (sourceFile, destinationFile);
				// To move an entire directory. To programmatically modify or combine
				// path strings, use the System.IO.Path class.
				System.IO.Directory.Move (sourceFile, destinationFile);
				Console.WriteLine ("Operation Complete");
				Main (args);
			} else if (c.Contains ("readTextFile")) {
				
				int counter = 0;
				string line;
				// Read the file and display it line by line.
				Console.WriteLine ("Specify path and file name + extension");
				string filepath = Console.ReadLine ();
				System.IO.StreamReader file =
					new System.IO.StreamReader (@filepath);
				Console.ForegroundColor = ConsoleColor.Green;
				while ((line = file.ReadLine ()) != null) {
					System.Console.WriteLine (line);
					counter++;
				}
				file.Close ();
				Console.ForegroundColor = ConsoleColor.Gray;
				System.Console.WriteLine ("There were {0} lines.", counter);
				// Suspend the screen.
				System.Console.ReadLine ();

				String[] args = { "args", "test" };
				Main (args);
			} else if (c.Contains ("addRegistryKey") || c.Contains ("addRegKey")) {
				RegistryKey key;
				Console.WriteLine ("Specify a subKey on current user to edit/Create");
				string subKey = Console.ReadLine ();
				Console.WriteLine ("Specify a value on current user to edit/Create");
				string value = Console.ReadLine ();
				Console.WriteLine ("Specify an object name on localMachine to create");
				string objName = Console.ReadLine ();
				key = Registry.LocalMachine.CreateSubKey (subKey);
				key.SetValue (value, objName);
				key.Close ();

				Console.WriteLine ("Operation_Complete");
				Console.WriteLine ("Loading");
				Console.Clear ();
				String[] args = { "args", "test" };
				Main (args);
			} else if (c.Contains ("Help") || c.Contains ("help")) {
				Console.WriteLine ("\nHelp - displays this help message.\naddRegKey - adds a key to the pc's registry\nreadTextFile - reads a specified text file" +
				"\nmove - moves a specified file\ncreate\\crte - creates a randomly generate file in a specified path" +
						"\ngetfiles\\GETFILES - makes a list of files in a specified folder or path.\nclear\\CLEAR\\cls - clears the console.\n" +
						"diskInfo - shows information about the computers hard-drive\nappDir - show the current applications directory\n\tthis is a useless" +
						"feature in this version but,\n\tlater the program will be able to migrate into restricted folders (eg : System32)");
				String[] args = { "args", "test" };
				Main (args);
			} else if (c.Contains ("clear")) {
				Console.Clear ();
					String[] args = { "args", "test" };
					Main (args);
				}else {
				BadCommand (c);
			}
		}

		public static void BadCommand(string c)
		{
			String[] arguments = { "Args", "test" };
			Console.WriteLine ("Unknown Command " + "'"+ c + "'");
			Main (arguments);
		}
	}
}
