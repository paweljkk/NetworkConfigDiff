//Pawel Klimek
//Network Sorting Tool

import java.io.File;
import java.io.PrintStream;
import java.nio.file.Paths;
import java.util.Scanner;

public class switchSorting
{
  public switchSorting() {}
  
  public static int configCounter = 0;
  public static int debugCounter = 0;
  
  public static void main(String[] args) {
    Scanner localScanner = new Scanner(System.in);
    
    System.out.println("Enter path to folder to sort:");
    String str1 = args[0];
    System.out.println("Enter path to folder to create sorted folder:");
    String str2 = args[1];
    if (str2.contains(str1))
    {
      System.out.println("\nPlease pick a location outside of the sort location");
      System.exit(0);
    }
    new File(str2 + "\\SortedSwitches").mkdir();
    String str3 = str2 + "\\SortedSwitches\\";
    File localFile = new File(str1);
    
    find_files(localFile, str3);
    System.out.println("!!! Number of Switch cfgs: " + configCounter);
    
    localScanner.close();
  }
  

  public static void find_files(File paramFile, String paramString)
  {
    File[] arrayOfFile1 = paramFile.listFiles();
    for (File localFile : arrayOfFile1)
    {
      if (localFile.isFile())
      {
        System.out.println(localFile.getName());
        if (!localFile.getName().contains("startup"))
        {
          try
          {
            Scanner localScanner = new Scanner(localFile);
            
            while (localScanner.hasNextLine()) {
              String str = localScanner.nextLine();
              if (str.contains("hostn"))
              {
                java.nio.file.Path localPath1 = Paths.get(paramString + str.substring(10, str.length() - 1).replace("\"", "") + ".txt", new String[] { "" });
                java.nio.file.Path localPath2 = Paths.get(paramFile + "\\" + localFile.getName(), new String[] { "" });
                
                java.nio.file.Files.copy(localPath2, localPath1, new java.nio.file.CopyOption[] { java.nio.file.StandardCopyOption.REPLACE_EXISTING });
                
                configCounter += 1;
              }
            }
          }
          catch (Exception localException) {}
        }
      }
      else if (localFile.isDirectory())
      {


        find_files(localFile, paramString);
      }
    }
  }
}