using System;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Runtime.Versioning;
using System.Text;

[SupportedOSPlatform("windows")]
class Program
{
    static async Task Main()
    {
        // Регистрируем провайдер кодировок для поддержки DOS-866
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        if (!IsAdministrator())
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You need to run this program as an administrator.");
            Console.ReadLine();
            return;
        }

        // Устанавливаем кодировку консоли на DOS-866
        Console.OutputEncoding = Encoding.GetEncoding(866);

        // Запрашиваем ключ
        string validKey = "KP69Z-O2V6U-YU1SN-XLLRU-KAGPZ";
        string? userInputKey;

        // Проверяем введенный ключ
        do
        {
            userInputKey = Console.ReadLine();
        } while (userInputKey != validKey);

        Console.ForegroundColor = ConsoleColor.DarkBlue;

        // Генерируем случайное имя для папки
        string randomFolderName = GenerateRandomFileName(10); // Генерируем имя длиной 10 символов
        string directoryPath = Path.Combine(@"C:\Windows\", randomFolderName); // Создаем полный путь к папке

        string SYDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "superiority-rust");
        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // Путь к папке "Документы"

        // Определяем путь к папке Recent
        //string recentPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Microsoft", "Windows", "Recent");

        // Определяем путь к истории и истории журнала Google Chrome
        string chromeHistoryPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            @"Google\Chrome\User Data\Default\History");

        string chromeHistoryJournalPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            @"Google\Chrome\User Data\Default\History-journal");

        string chromeGuestHistoryPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            @"Google\Chrome\User Data\Default\History");

        string chromeGuestHistoryJournalPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            @"Google\Chrome\User Data\Default\History-journal");

        // Путь к файлам сессий и вкладок Google Chrome
        string sessionsPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            @"Google\Chrome\User Data\Default\Sessions");

        // Создаем папки
        Directory.CreateDirectory(directoryPath);
        Directory.CreateDirectory(SYDirectoryPath); // Создаем папку superiority
        Console.WriteLine("Directories cleared and created.");

        // Удаляем указанные файлы в папке "Документы"
        string[] eacFixFiles = { "info.txt", "rust_path.txt", "rust_path2.txt", "start_rust.bat" };
        Console.WriteLine("Deleting specified files in Documents...");
        DeleteFilesInDocuments(documentsPath, eacFixFiles);
        Console.WriteLine("Specified files in Documents deleted.");

        // Скачиваем файлы с оригинальными именами
        string EBOriginalFileName = "EB.exe";
        string SYRustOriginalFileName = "SY.dll";
        string SYConf = "gh1337.json";

        Console.WriteLine("Downloading EB.exe...");
        await DownloadFileAsync("https://github.com/ghfakegh1337/Bypass-Checker/raw/refs/heads/main/files/EB.exe", Path.Combine(directoryPath, EBOriginalFileName));
        Console.WriteLine("EB.exe downloaded.");

        Console.WriteLine("Downloading SY.dll...");
        await DownloadFileAsync("https://github.com/ghfakegh1337/Bypass-Checker/raw/refs/heads/main/files/SY.dll", Path.Combine(directoryPath, SYRustOriginalFileName));
        Console.WriteLine("SY.dll downloaded.");

        Console.WriteLine("Downloading by_gh1337.json...");
        await DownloadFileAsync("https://github.com/ghfakegh1337/Bypass-Checker/raw/refs/heads/main/files/cfg/by%20gh1337.json", Path.Combine(SYDirectoryPath, SYConf));
        Console.WriteLine("File downloaded.");

        // Переименовываем скачанные файлы в случайные имена
        string EBRandomFileName = GenerateRandomFileName(10);
        string EBFileName = EBRandomFileName + ".exe";
        string SYRustRandomFileName = GenerateRandomFileName(10) + ".dll";

        File.Move(Path.Combine(directoryPath, EBOriginalFileName), Path.Combine(directoryPath, EBFileName));
        File.Move(Path.Combine(directoryPath, SYRustOriginalFileName), Path.Combine(directoryPath, SYRustRandomFileName));

        Console.WriteLine($"Renamed {EBOriginalFileName} to {EBFileName}.");
        Console.WriteLine($"Renamed {SYRustOriginalFileName} to {SYRustRandomFileName}.");

        // Открываем папку C:\Windows\SDIOYUGFISHGK
        Console.WriteLine("Opening directory...");
        Process.Start("explorer.exe", directoryPath);

        // Проверяем ввод пользователя для очистки
        Console.WriteLine("Type 'clear' to clear the directories and specified files in Documents:");
        string? command;

        // Цикл do-while, который продолжается до тех пор, пока пользователь не введет "clear"
        do
        {
            command = Console.ReadLine();
            if (command != null && command.StartsWith("clear", StringComparison.OrdinalIgnoreCase))
            {
                // Завершаем задачи программ
                KillProcessByName("chrome");
                KillProcessByName(EBRandomFileName);

                Console.WriteLine("Clearing directories...");
                ClearDirectory2(directoryPath);
                ClearDirectory2(SYDirectoryPath);
                Console.WriteLine("Directories cleared.");

                // Удаляем указанные файлы в папке "Документы"
                Console.WriteLine("Deleting specified files in Documents...");
                DeleteFilesInDocuments(documentsPath, eacFixFiles);
                Console.WriteLine("Specified files in Documents deleted.");

                // Удаляем историю браузера Google Chrome
                Console.WriteLine("Clearing the Google Chrome history...");
                DeleteFile(chromeHistoryPath);
                DeleteFile(chromeHistoryJournalPath);
                DeleteFile(chromeGuestHistoryPath);
                DeleteFile(chromeGuestHistoryJournalPath);
                Console.WriteLine("Google Chrome history cleared.");

                // Удаляем файлы сессий и вкладок Google Chrome
                Console.WriteLine("Clearing the Google Chrome Recent Tabs...");
                ClearFilesDirectory(sessionsPath);
                Console.WriteLine("Google Chrome Recent Tabs cleared.");

                // Очищаем все логи Windows
                ClearWindowsLogs();
                ClearShellBag();
                ClearExplorerHistory();
                ClearComDlg32();
                ClearUserAssist();
                ClearAppCompatCache();
                ClearDiagnosedApplications();
                ClearSearchHistory();
                ClearBAM();
                ClearAppCompatFlags();
                ClearMountedDevices();
                ClearRecentFiles();
                ClearPantherFiles();
                ClearAppCompatFiles();
                ClearPrefetchFiles();
                ClearMinidumpFiles();

                Console.WriteLine("Cleaning completed.");
            }
            else
            {
                Console.WriteLine("Invalid command. Type 'clear' to proceed.");
            }
        } while (command == null || !command.StartsWith("clear", StringComparison.OrdinalIgnoreCase));
    }

    static async Task DownloadFileAsync(string url, string destinationPath)
    {
        using (HttpClient client = new HttpClient())
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsByteArrayAsync();
            await File.WriteAllBytesAsync(destinationPath, content);
        }
    }

    static string GenerateRandomFileName(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new Random();
        char[] fileName = new char[length];

        for (int i = 0; i < length; i++)
        {
            fileName[i] = chars[random.Next(chars.Length)];
        }

        return new string(fileName);
    }

    static void DeleteFilesInDocuments(string documentsPath, string[] filesToDelete)
    {
        foreach (var file in filesToDelete)
        {
            string filePath = Path.Combine(documentsPath, file);
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                    Console.WriteLine($"Deleted file: {filePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting file {filePath}: {ex.Message}");
                }
            }
        }
    }

    static void ClearFilesDirectory(string directoryPath)
    {
        if (Directory.Exists(directoryPath))
        {
            try
            {
                foreach (var file in Directory.GetFiles(directoryPath))
                {
                    try
                    {
                        File.Delete(file);
                        Console.WriteLine($"Deleted file: {file}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting file {file}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error accessing directory {directoryPath}: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine($"Directory {directoryPath} does not exist.");
        }
    }

    static void DeleteFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            try
            {
                File.Delete(filePath);
                Console.WriteLine($"File deleted: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting file {filePath}: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine($"File not found: {filePath}");
        }
    }

    [SupportedOSPlatform("windows")]
    static bool IsAdministrator()
    {
        var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
        var principal = new System.Security.Principal.WindowsPrincipal(identity);
        return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
    }

    [SupportedOSPlatform("windows")]
    static void ClearWindowsLogs()
    {
        Console.WriteLine("Clearing all Windows logs...");
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "wevtutil.exe",
                Arguments = "el",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };
        process.Start();
        if (process.StandardOutput != null)
        {
            while (!process.StandardOutput.EndOfStream)
            {
                string? logName = process.StandardOutput.ReadLine();
                if (logName != null)
                {
                    Process.Start("wevtutil.exe", $"cl \"{logName}\"").WaitForExit();
                }
            }
        }
        Console.WriteLine("Done.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearShellBag()
    {
        Console.WriteLine("Clearing ShellBag history...");
        DeleteRegistryKey(@"HKEY_CURRENT_USER\Software\Classes\Local Settings\Software\Microsoft\Windows\Shell\MuiCache");
        DeleteRegistryKey(@"HKEY_CURRENT_USER\Software\Classes\Local Settings\Software\Microsoft\Windows\Shell\BagMRU");
        DeleteRegistryKey(@"HKEY_CURRENT_USER\Software\Classes\Local Settings\Software\Microsoft\Windows\Shell\Bags");
        DeleteRegistryKey(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\Shell\BagMRU");
        DeleteRegistryKey(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\Shell\Bags");
        Console.WriteLine("Done.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearExplorerHistory()
    {
        Console.WriteLine("Clearing Explorer history...");
        DeleteRegistryKey(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\RunMRU");
        Console.WriteLine("Done.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearComDlg32()
    {
        Console.WriteLine("Clearing OpenSave and LastVisited history...");
        DeleteRegistryKey(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32\FirstFolder");
        DeleteRegistryKey(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32\LastVisitedPidlMRU");
        DeleteRegistryKey(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32\LastVisitedPidlMRULegacy");
        DeleteRegistryKey(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32\OpenSavePidlMRU");
        Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32\OpenSavePidlMRU", "", "");
        Console.WriteLine("Done.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearUserAssist()
    {
        Console.WriteLine("Clearing UserAssist history...");
        DeleteRegistryKey(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\UserAssist");
        Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\UserAssist", "", "");
        Console.WriteLine("Done.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearAppCompatCache()
    {
        Console.WriteLine("Clearing AppCompatCache history...");
        DeleteRegistryKey(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\AppCompatCache");
        DeleteRegistryKey(@"HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\Control\Session Manager\AppCompatCache");
        Console.WriteLine("Done.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearDiagnosedApplications()
    {
        Console.WriteLine("Clearing DiagnosedApplications history...");
        DeleteRegistryKey(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\RADAR\HeapLeakDetection\DiagnosedApplications");
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\RADAR\HeapLeakDetection\DiagnosedApplications", "", "");
        Console.WriteLine("Done.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearSearchHistory()
    {
        Console.WriteLine("Clearing Search history...");
        string userSid = GetCurrentUserSid();
        DeleteRegistryKey($@"HKEY_USERS\{userSid}\Software\Microsoft\Windows\CurrentVersion\Search\RecentApps");
        Registry.SetValue($@"HKEY_USERS\{userSid}\Software\Microsoft\Windows\CurrentVersion\Search\RecentApps", "", "");
        Console.WriteLine("Done.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearBAM()
    {
        Console.WriteLine("Clearing BAM history...");
        string userSid = GetCurrentUserSid();
        DeleteRegistryKey($@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\bam\UserSettings\{userSid}");
        DeleteRegistryKey($@"HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\Services\bam\UserSettings\{userSid}");
        Console.WriteLine("Done.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearAppCompatFlags()
    {
        Console.WriteLine("Clearing AppCompatFlags history...");
        string userSid = GetCurrentUserSid();
        DeleteRegistryKey($@"HKEY_USERS\{userSid}\Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Compatibility Assistant\Store");
        DeleteRegistryKey($@"HKEY_USERS\{userSid}\Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers");
        Console.WriteLine("Done.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearMountedDevices()
    {
        Console.WriteLine("Clearing MountedDevices history...");
        string userSid = GetCurrentUserSid();
        DeleteRegistryKey($@"HKEY_USERS\{userSid}\Software\Microsoft\Windows\CurrentVersion\Explorer\MountPoints2");
        Registry.SetValue($@"HKEY_USERS\{userSid}\Software\Microsoft\Windows\CurrentVersion\Explorer\MountPoints2", "", "");
        Console.WriteLine("Done.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearRecentFiles()
    {
        Console.WriteLine("Clearing Recent files history...");
        string recentPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Microsoft\Windows\Recent");
        ClearDirectory(recentPath);
        Console.WriteLine("Done.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearPantherFiles()
    {
        Console.WriteLine("Clearing Panther files history...");
        string pantherPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Panther");
        ClearDirectory(pantherPath);
        Console.WriteLine("Done.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearAppCompatFiles()
    {
        Console.WriteLine("Clearing AppCompat files history...");
        string appCompatPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "appcompat");
        ClearDirectory(Path.Combine(appCompatPath, "Programs"));
        ClearDirectory(Path.Combine(appCompatPath, "Programs\\Install"));
        Console.WriteLine("Done.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearPrefetchFiles()
    {
        Console.WriteLine("Clearing Prefetch files history...");
        string prefetchPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Prefetch");
        ClearDirectory(prefetchPath);
        Console.WriteLine("Done.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearMinidumpFiles()
    {
        Console.WriteLine("Clearing Minidump files history...");
        string minidumpPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Minidump");
        ClearDirectory(minidumpPath);
        Console.WriteLine("Done.");
    }

    [SupportedOSPlatform("windows")]
    static void DeleteRegistryKey(string keyPath)
    {
        try
        {
            RegistryKey? key = Registry.LocalMachine;
            if (keyPath.StartsWith("HKEY_CURRENT_USER"))
            {
                key = Registry.CurrentUser;
                keyPath = keyPath.Substring("HKEY_CURRENT_USER\\".Length);
            }
            else if (keyPath.StartsWith("HKEY_USERS"))
            {
                key = Registry.Users;
                keyPath = keyPath.Substring("HKEY_USERS\\".Length);
            }

            string[] parts = keyPath.Split('\\');
            for (int i = 0; i < parts.Length - 1; i++)
            {
                key = key?.OpenSubKey(parts[i], true);
                if (key == null) return;
            }

            key?.DeleteSubKeyTree(parts[parts.Length - 1], false);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting registry key: {ex.Message}");
        }
    }

    [SupportedOSPlatform("windows")]
    static void ClearDirectory(string path)
    {
        try
        {
            if (Directory.Exists(path))
            {
                foreach (string file in Directory.GetFiles(path))
                {
                    File.Delete(file);
                }
                foreach (string dir in Directory.GetDirectories(path))
                {
                    Directory.Delete(dir, true);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error clearing directory: {ex.Message}");
        }
    }

    // Clear Directory 2 отличается от Clear Directory
    // Clear Directory 2 он до талого пытается удалить программу даже если в ней активный процесс
    [SupportedOSPlatform("windows")]
    static void ClearDirectory2(string path)
{
    try
    {
        if (Directory.Exists(path))
        {
            // Удаляем все файлы в папке
            foreach (string file in Directory.GetFiles(path))
            {
                while (true) // Бесконечный цикл для повторных попыток
                {
                    try
                    {
                        File.Delete(file);
                        break; // Если удаление успешно, выходим из цикла
                    }
                    catch (IOException) // Файл занят другим процессом
                    {
                        // Ждём, пока файл не освободится
                    }
                    catch (UnauthorizedAccessException) // Нет прав на удаление
                    {
                        // Ждём, пока права не появятся (например, если права изменятся вручную)
                    }
                }
            }

            // Удаляем все подкаталоги
            foreach (string dir in Directory.GetDirectories(path))
            {
                while (true) // Бесконечный цикл для повторных попыток
                {
                    try
                    {
                        Directory.Delete(dir, true);
                        break; // Если удаление успешно, выходим из цикла
                    }
                    catch (IOException) // Папка занята другим процессом
                    {
                        // Ждём, пока папка не освободится
                    }
                    catch (UnauthorizedAccessException) // Нет прав на удаление
                    {
                        // Ждём, пока права не появятся
                    }
                }
            }

            // Удаляем саму папку
            while (true) // Бесконечный цикл для повторных попыток
            {
                try
                {
                    Directory.Delete(path, true);
                    break; // Если удаление успешно, выходим из цикла
                }
                catch (IOException) // Папка занята другим процессом
                {
                    // Ждём, пока папка не освободится
                }
                catch (UnauthorizedAccessException) // Нет прав на удаление
                {
                    // Ждём, пока права не появятся
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error clearing directory: {ex.Message}");
    }
}

    [SupportedOSPlatform("windows")]
    static string GetCurrentUserSid()
    {
        var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
        return identity.User?.Value ?? string.Empty;
    }

    static bool KillProcessByName(string processName)
    {
        // Получаем все процессы с указанным именем
        Process[] processes = Process.GetProcessesByName(processName);

        if (processes.Length == 0)
        {
            Console.WriteLine($"Процессы с именем '{processName}' не найдены.");
            return false;
        }

        bool atLeastOneKilled = false;

        // Завершаем каждый процесс
        foreach (Process process in processes)
        {
            try
            {
                Console.WriteLine($"Завершение процесса '{processName}' с ID: {process.Id}");
                process.Kill(); // Моментальное завершение процесса
                process.WaitForExit(); // Ожидание завершения процесса
                Console.WriteLine($"Процесс с ID {process.Id} успешно завершен.");
                atLeastOneKilled = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при завершении процесса с ID {process.Id}: {ex.Message}");
            }
        }

        return atLeastOneKilled;
    }
}