using System;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Runtime.Versioning;
using System.Text;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Reflection;
using System.Net;
using System.Threading;


[SupportedOSPlatform("windows")]
class Program
{
    static async Task Main()
    {
        CheckAdministratorPrivileges();
        ValidateInputKey();

        char gameChoice = '0';
        SelectGame(ref gameChoice);

        string directoryPath = Path.Combine(@"C:\Windows\", "StealthCore"); // Создаем полный путь к папке

        ForceClearDirectory(directoryPath);

        if (gameChoice == '1')
        {
            await Rust(directoryPath);
        }
        else if (gameChoice == '2')
        {
            await gta5(directoryPath);
        }
    }

    static void CheckAdministratorPrivileges()
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
    }

    static void ValidateInputKey()
    {
        // Запрашиваем ключ
        string validKey = "KP69Z-O2V6U-YU1SN-XLLRU-KAGPZ";
        string? userInputKey;

        // Проверяем введенный ключ
        do
        {
            userInputKey = Console.ReadLine();
        } while (userInputKey != validKey);

        Console.ForegroundColor = ConsoleColor.DarkRed;
    }

    static void SelectGame(ref char gameChoice)
    {
        while (true)
        {
            Console.WriteLine("Choose a game:");
            Console.WriteLine("Rusticaland (1)");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            gameChoice = keyInfo.KeyChar;

            if (gameChoice == '1')
            {
                break; // Если введено 1 или 2, выходим из цикла
            }
            else
            {
                Console.WriteLine("Mistake! Select 1 (Rusticaland). Try again.");
            }
        }
    }

    static async Task Rust(string directoryPath)
    {
        // Создаем важную папку
        Directory.CreateDirectory(directoryPath);

        // Пути к папкам
        string superiorityDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "superiority-rust");

        // Создаем папку superiority
        Directory.CreateDirectory(superiorityDirectoryPath);

        // Определяем файлы с оригинальными именами
        string RustInjectorOriginalFileName = "Inj.exe";
        string RustInjectorSettingsOriginalFileName = "settings.xml";
        string superiorityRustOriginalFileName = "SY.dll";

        Console.WriteLine("Download the Injector...");
        await DownloadFileAsync("https://github.com/ghfakegh1337/StealthCore/raw/refs/heads/main/files/rust/Inj.exe", Path.Combine(directoryPath, RustInjectorOriginalFileName));
        Console.WriteLine("The injector is downloaded.");

        Console.WriteLine("Download the Injector Settings...");
        await DownloadFileAsync("https://github.com/ghfakegh1337/StealthCore/raw/refs/heads/main/files/rust/settings.xml", Path.Combine(directoryPath, RustInjectorSettingsOriginalFileName));
        Console.WriteLine("The injector settings is downloaded.");

        Console.WriteLine("Downloading Superiority...");
        await DownloadFileAsync("https://github.com/ghfakegh1337/StealthCore/raw/refs/heads/main/files/rust/SY.dll", Path.Combine(directoryPath, superiorityRustOriginalFileName));
        Console.WriteLine("Superiority has been downloaded.");

        Console.WriteLine("Downloading configs...");
        await DownloadGitHubFolder("https://github.com/ghfakegh1337/StealthCore/tree/main/files/rust/cfg", superiorityDirectoryPath);
        Console.WriteLine("Configs has been downloaded.");

        // Переименовываем скачанные файлы в случайные имена
        string rustInjectorSavedFileName = GenerateRandomFileName(10);
        string RustInjectorRandomFileName = rustInjectorSavedFileName + ".exe";
        string SYRustRandomFileName = GenerateRandomFileName(10) + ".dll";
        File.Move(Path.Combine(directoryPath, RustInjectorOriginalFileName), Path.Combine(directoryPath, RustInjectorRandomFileName));
        File.Move(Path.Combine(directoryPath, superiorityRustOriginalFileName), Path.Combine(directoryPath, SYRustRandomFileName));
        Console.WriteLine($"{RustInjectorOriginalFileName} renamed to {RustInjectorRandomFileName}.");
        Console.WriteLine($"{superiorityRustOriginalFileName} renamed to {SYRustRandomFileName}.");

        // Открываем важную папку
        Console.WriteLine("Opening the main folder...");
        Process.Start("explorer.exe", directoryPath);

        // Проверяем ввод пользователя для очистки
        Console.WriteLine("Type 'clear' to clean:");
        Console.WriteLine("Type 'folder' to open the cheat folder:");
        Console.WriteLine("Type 'exit' if you want to exit the program without deleting it or clearing anything: ");
        string? command;

        // Цикл do-while, который продолжается до тех пор, пока пользователь не введет "clear"
        do
        {
            command = Console.ReadLine();
            if (command != null && command.StartsWith("clear", StringComparison.OrdinalIgnoreCase))
            {
                // Завершаем задачи программ
                KillProcessByName(rustInjectorSavedFileName);

                // Чистка папок
                Console.WriteLine("Cleaning folders...");
                ForceClearDirectory(directoryPath);
                ForceClearDirectory(superiorityDirectoryPath);
                ClearNvidiaControlPanel();
                Console.WriteLine("Folders have been cleaned.");

                ClearWindowsLogs();

                ResetNetworkUsageStats();

                SelfDestruct();

                Console.WriteLine("Cleanup completed successfully.");
            }
            else if (command != null && command.StartsWith("folder", StringComparison.OrdinalIgnoreCase))
            {
                Process.Start("explorer.exe", directoryPath);
            }
            else if (command != null && command.StartsWith("exit", StringComparison.OrdinalIgnoreCase))
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("You entered an invalid command. Please type 'clear/folder/exit'.");
            }

        } while (command == null || !command.StartsWith("clear", StringComparison.OrdinalIgnoreCase));
    }

    static async Task gta5(string directoryPath)
    {
        // Создаем важную папку
        Directory.CreateDirectory(directoryPath);

        // Путь к папке
        string amphetamineCSGODirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AMTH.CSGO");

        // Определяем файлы с оригинальными именами
        string amphetamineLoaderOriginalFileName = "Loader.exe";

        // Скачиваем файлы с оригинальными именами
        Console.WriteLine("Downloading Amphetamine Loader...");
        await DownloadFileAsync("https://github.com/ghfakegh1337/StealthCore/raw/refs/heads/main/files/gta5/Loader.exe", Path.Combine(directoryPath, amphetamineLoaderOriginalFileName));
        Console.WriteLine("Amphetamine Loader downloaded.");

        // Переименовываем скачанные файлы в случайные имена
        string amphetamineLoaderSavedFileName = GenerateRandomFileName(10);
        string amphetamineLoaderRandomFileName = amphetamineLoaderSavedFileName + ".exe";

        File.Move(Path.Combine(directoryPath, amphetamineLoaderOriginalFileName), Path.Combine(directoryPath, amphetamineLoaderRandomFileName));

        Console.WriteLine($"Renamed {amphetamineLoaderOriginalFileName} to {amphetamineLoaderRandomFileName}.");

        // Открываем важную папку
        Console.WriteLine("Opening the main folder...");
        Process.Start("explorer.exe", directoryPath);

        // Проверяем ввод пользователя для очистки
        Console.WriteLine("Type 'clear' to clean:");
        Console.WriteLine("Type 'folder' to open the cheat folder:");
        string? command;

        // Цикл do-while, который продолжается до тех пор, пока пользователь не введет "clear/folder"
        do
        {
            command = Console.ReadLine();
            if (command != null && command.StartsWith("clear", StringComparison.OrdinalIgnoreCase))
            {
                // Завершаем задачи программ
                KillProcessByName(amphetamineLoaderSavedFileName);
                
                // Чистка папок
                Console.WriteLine("Cleaning folders...");
                ForceClearDirectory(directoryPath);
                ForceClearDirectory(amphetamineCSGODirectoryPath);
                ClearNvidiaControlPanel();
                Console.WriteLine("Folders cleaned.");

                ClearWindowsLogs();

                ResetNetworkUsageStats();

                SelfDestruct();

                Console.WriteLine("Cleanup completed successfully.");
            }
            else if (command != null && command.StartsWith("folder", StringComparison.OrdinalIgnoreCase))
            {
                Process.Start("explorer.exe", directoryPath);
            }
            else
            {
                Console.WriteLine("You entered an invalid command. Please type 'clear' or 'folder'.");
            }

        } while (command == null || !command.StartsWith("clear", StringComparison.OrdinalIgnoreCase));
    }
    static void ClearWindowsLogs()
    {
        // Очищаем все логи Windows
        ClearWindowsEventLogs();
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

    static void ClearNvidiaControlPanel()
    {
        ClearFilesDirectory(@"C:\ProgramData\NVIDIA Corporation\Drs");
    }

    public static async Task DownloadGitHubFolder(string url, string savePath)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.UserAgent.ParseAdd("MyApp/1.0");

        try
        {
            var uri = new Uri(url);
            var segments = uri.AbsolutePath.Trim('/').Split('/');
            var apiUrl = $"https://api.github.com/repos/{segments[0]}/{segments[1]}/contents/{string.Join("/", segments[4..])}";

            var response = await client.GetStreamAsync(apiUrl);
            var files = await JsonSerializer.DeserializeAsync<List<Dictionary<string, JsonElement>>>(response);

            Directory.CreateDirectory(savePath);

            // Добавлена проверка на null
            if (files != null)
            {
                foreach (var file in files)
                {
                    // Добавлены проверки на наличие ключей и null
                    if (file.TryGetValue("type", out var typeElement) 
                        && typeElement.GetString() == "file"
                        && file.TryGetValue("name", out var nameElement)
                        && file.TryGetValue("download_url", out var urlElement))
                    {
                        var fileName = nameElement.GetString();
                        var downloadUrl = urlElement.GetString();

                        // Проверка на null для fileName и downloadUrl
                        if (fileName == null || downloadUrl == null)
                        {
                            Console.WriteLine("Skipping file due to missing name or URL.");
                            continue;
                        }

                        var filePath = Path.Combine(savePath, fileName);
                        var fileBytes = await client.GetByteArrayAsync(downloadUrl);
                        await File.WriteAllBytesAsync(filePath, fileBytes);
                        Console.WriteLine($"Downloaded: {fileName}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
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
                        Console.WriteLine($"File deleted: {file}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to delete the file {file}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error accessing the directory {directoryPath}: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine($"The directory {directoryPath} does not exist.");
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
    static void ClearWindowsEventLogs()
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
        Console.WriteLine("Cleaning OpenSave and LastVisited history...");
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
        Console.WriteLine("Cleaning UserAssist history...");
        DeleteRegistryKey(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\UserAssist");
        Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\UserAssist", "", "");
        Console.WriteLine("Done.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearAppCompatCache()
    {
        Console.WriteLine("Cleaning AppCompatCache history...");
        DeleteRegistryKey(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\AppCompatCache");
        DeleteRegistryKey(@"HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\Control\Session Manager\AppCompatCache");
        Console.WriteLine("Done.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearDiagnosedApplications()
    {
        Console.WriteLine("Cleaning DiagnosedApplications history...");
        DeleteRegistryKey(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\RADAR\HeapLeakDetection\DiagnosedApplications");
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\RADAR\HeapLeakDetection\DiagnosedApplications", "", "");
        Console.WriteLine("Done.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearSearchHistory()
    {
        Console.WriteLine("Cleaning Search history...");
        string userSid = GetCurrentUserSid();
        DeleteRegistryKey($@"HKEY_USERS\{userSid}\Software\Microsoft\Windows\CurrentVersion\Search\RecentApps");
        Registry.SetValue($@"HKEY_USERS\{userSid}\Software\Microsoft\Windows\CurrentVersion\Search\RecentApps", "", "");
        Console.WriteLine("Done.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearBAM()
    {
        Console.WriteLine("Cleaning BAM history...");
        string userSid = GetCurrentUserSid();
        DeleteRegistryKey($@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\bam\UserSettings\{userSid}");
        DeleteRegistryKey($@"HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\Services\bam\UserSettings\{userSid}");
        Console.WriteLine("Done.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearAppCompatFlags()
    {
        Console.WriteLine("Cleaning AppCompatFlags history...");
        string userSid = GetCurrentUserSid();
        DeleteRegistryKey($@"HKEY_USERS\{userSid}\Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Compatibility Assistant\Store");
        DeleteRegistryKey($@"HKEY_USERS\{userSid}\Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers");
        Console.WriteLine("Done.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearMountedDevices()
    {
        Console.WriteLine("Cleaning MountedDevices history...");
        string userSid = GetCurrentUserSid();
        DeleteRegistryKey($@"HKEY_USERS\{userSid}\Software\Microsoft\Windows\CurrentVersion\Explorer\MountPoints2");
        Registry.SetValue($@"HKEY_USERS\{userSid}\Software\Microsoft\Windows\CurrentVersion\Explorer\MountPoints2", "", "");
        Console.WriteLine("Done.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearRecentFiles()
    {
        Console.WriteLine("Cleaning Recent history files...");
        string recentPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Microsoft\Windows\Recent");
        ClearDirectory(recentPath);
        Console.WriteLine("Done.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearPantherFiles()
    {
        Console.WriteLine("Cleaning Panther history files...");
        string pantherPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Panther");
        ClearDirectory(pantherPath);
        Console.WriteLine("Done.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearAppCompatFiles()
    {
        Console.WriteLine("Cleaning AppCompat history files...");
        string appCompatPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "appcompat");
        ClearDirectory(Path.Combine(appCompatPath, "Programs"));
        ClearDirectory(Path.Combine(appCompatPath, "Programs\\Install"));
        Console.WriteLine("Done.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearPrefetchFiles()
    {
        Console.WriteLine("Cleaning Prefetch history files...");
        string prefetchPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Prefetch");
        ClearDirectory(prefetchPath);
        Console.WriteLine("Done.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearMinidumpFiles()
    {
        Console.WriteLine("Cleaning Minidump history files...");
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
            Console.WriteLine($"Failed to delete the registry key: {ex.Message}");
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
            Console.WriteLine($"Failed to delete the folder: {ex.Message}");
        }
    }

    [SupportedOSPlatform("windows")]
    static void ForceClearDirectory(string path)
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
            Console.WriteLine($"Failed to delete the folder: {ex.Message}");
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
            Console.WriteLine($"Processes with name '{processName}' not found.");
            return false;
        }

        bool atLeastOneKilled = false;

        // Завершаем каждый процесс
        foreach (Process process in processes)
        {
            try
            {
                Console.WriteLine($"Terminating process '{processName}' with ID: {process.Id}");
                process.Kill(); // Моментальное завершение процесса
                process.WaitForExit(); // Ожидание завершения процесса
                Console.WriteLine($"Process with ID {process.Id} successfully terminated.");
                atLeastOneKilled = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error terminating process with ID {process.Id}: {ex.Message}");
            }
        }

        return atLeastOneKilled;
    }

    static void ResetNetworkUsageStats()
    {
        string sruDbPath = @"C:\Windows\System32\sru\SRUDB.dat";

        try
        {
            // Останавливаем службу Диагностической политики асинхронно
            Task stopServiceTask = Task.Run(() => RunProcess("net", "stop DPS"));

            // Удаляем файл, пока служба останавливается
            if (File.Exists(sruDbPath))
            {
                try
                {
                    File.Delete(sruDbPath);
                    Console.WriteLine("Network usage statistics have been successfully reset.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to delete SRUDB.dat: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("The SRUDB.dat file was not found.");
            }

            // Дожидаемся остановки службы
            stopServiceTask.Wait();

            // Запускаем службу обратно в отдельном потоке
            Task.Run(() => RunProcess("net", "start DPS"));
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error has occurred: " + ex.Message);
        }
    }

    static void RunProcess(string fileName, string arguments)
    {
        try
        {
            using (Process process = new Process())
            {
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = arguments,
                    UseShellExecute = true,
                    Verb = "runas", // Запуск от имени администратора
                    CreateNoWindow = true
                };

                process.Start();
                process.WaitForExit();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to execute command {fileName} {arguments}: {ex.Message}");
        }
    }


    public static void SelfDestruct()
    {
        try
        {
            string targetFileName = "Setup.exe";
            string exePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, targetFileName);

            string batchScript = @"
                @echo off
                :loop
                tasklist | find /i ""{{EXE_NAME}}"" >nul 2>&1
                if not errorlevel 1 (
                    timeout /t 1 /nobreak >nul
                    goto loop
                )
            
                del /f /q ""{{EXE_PATH}}"" >nul 2>&1
                del /f /q ""{{BATCH_PATH}}"" >nul 2>&1
                exit";

            string tempPath = Path.GetTempFileName() + ".bat";

            batchScript = batchScript
                .Replace("{{EXE_NAME}}", targetFileName)
                .Replace("{{EXE_PATH}}", exePath)
                .Replace("{{BATCH_PATH}}", tempPath);

            File.WriteAllText(tempPath, batchScript);

            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/C start /B \"\" \"{tempPath}\"",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                UseShellExecute = false
            });
        }
        catch (Exception ex)
        {
            // Обработка ошибок
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}