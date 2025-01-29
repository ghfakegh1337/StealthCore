using System;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Runtime.Versioning;
using System.Text;
using System.Runtime.CompilerServices;

[SupportedOSPlatform("windows")]
class Program
{
    static async Task Main()
    {
        проверкаНаАдминистратора();
        проверкаНаВведенныйКлюч();

        char какаяИгра = '0';
        выборИгры(ref какаяИгра);

        // Генерируем случайное имя для важной папки
        string randomFolderName = GenerateRandomFileName(10); // Генерируем имя длиной 10 символов
        string directoryPath = Path.Combine(@"C:\Windows\", randomFolderName); // Создаем полный путь к папке

        // Определяем путь к папке Recent
        //string recentPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Microsoft", "Windows", "Recent");

        if (какаяИгра == '1')
        {
            await Rust(directoryPath);
        }
        else if (какаяИгра == '2')
        {
            await CS2(directoryPath);
        }
        else
        {
            Console.WriteLine("Вы выбрали не верную игру");
            Console.WriteLine("Выполняю очистку");

            // Завершаем задачи программ
            KillProcessByName("chrome");
                
            // Чистка папок
            Console.WriteLine("Clearing directories...");
            ClearDirectory2(directoryPath);
            Console.WriteLine("Directories cleared.");

            удаляемИсториюИСессииВкладокGoogleChrome();

            очисткаЛоговWindows();

            Console.WriteLine("Cleaning completed.");
        }
    }

    static void проверкаНаАдминистратора()
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

    static void проверкаНаВведенныйКлюч()
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

    static void выборИгры(ref char какаяИгра)
    {
        Console.WriteLine("Введите в какую игру вы будете играть:");
        Console.WriteLine("Rust (1)");
        Console.WriteLine("CS2 (2)");
        ConsoleKeyInfo keyInfo = Console.ReadKey(true); // Читаем ключ и скрываем его отображение на экране
        какаяИгра = keyInfo.KeyChar;
    }

    static async Task Rust(string directoryPath)
    {
        // Создаем важную папку
        Directory.CreateDirectory(directoryPath);

        // Пути к папкам
        string superiorityDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "superiority-rust");
        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        // Создаем папку superiority
        Directory.CreateDirectory(superiorityDirectoryPath); // Создаем папку superiority

        // Удаляем указанные файлы в папке "Документы"
        Console.WriteLine("Удаляем файлы из документов...");
        string[] eacFixFiles = { "info.txt", "rust_path.txt", "rust_path2.txt", "start_rust.bat" };
        DeleteFilesInDocuments(documentsPath, eacFixFiles);
        Console.WriteLine("Файлы из документов удалены.");

        // Определяем файлы с оригинальными именами
        string EacBypassOriginalFileName = "EB.exe";
        //string RustInjectorOriginalFileName = "Inj.exe";
        string superiorityRustOriginalFileName = "SY.dll";
        string superiorityConf = "gh1337.json";

        // Скачиваем файлы с оригинальными именами
        Console.WriteLine("Скачиваем Eac Bypass...");
        await DownloadFileAsync("https://github.com/ghfakegh1337/StealthCore/raw/refs/heads/main/files/rust/EB.exe", Path.Combine(directoryPath, EacBypassOriginalFileName));
        Console.WriteLine("Eac Bypass скачен.");
        //Console.WriteLine("Скачиваем Injector...");
        //await DownloadFileAsync("https://github.com/ghfakegh1337/StealthCore/raw/refs/heads/main/files/rust/Inj.exe", Path.Combine(directoryPath, RustInjectorOriginalFileName));
        //Console.WriteLine("Injector скачен.");
        Console.WriteLine("Скачиваем Superiority...");
        await DownloadFileAsync("https://github.com/ghfakegh1337/StealthCore/raw/refs/heads/main/files/rust/SY.dll", Path.Combine(directoryPath, superiorityRustOriginalFileName));
        Console.WriteLine("Superiority скачен.");
        Console.WriteLine("Скачиваем конфиг...");
        await DownloadFileAsync("https://github.com/ghfakegh1337/StealthCore/raw/refs/heads/main/files/rust/cfg/by%20gh1337.json", Path.Combine(superiorityDirectoryPath, superiorityConf));
        Console.WriteLine("Конфиг скачен.");

        // Переименовываем скачанные файлы в случайные имена
        string EacBypassСохряненоеФайловоеИмя = GenerateRandomFileName(10);
        string EacBypassRandomFileName = EacBypassСохряненоеФайловоеИмя + ".exe";
        string RustInjectorСохряненоеФайловоеИмя = GenerateRandomFileName(10);
        string RustInjectorRandomFileName = RustInjectorСохряненоеФайловоеИмя + ".exe";
        string SYRustRandomFileName = GenerateRandomFileName(10) + ".dll";
        File.Move(Path.Combine(directoryPath, EacBypassOriginalFileName), Path.Combine(directoryPath, EacBypassRandomFileName));
        //File.Move(Path.Combine(directoryPath, RustInjectorOriginalFileName), Path.Combine(directoryPath, RustInjectorRandomFileName));
        File.Move(Path.Combine(directoryPath, superiorityRustOriginalFileName), Path.Combine(directoryPath, SYRustRandomFileName));
        Console.WriteLine($"Переименован {EacBypassOriginalFileName} в {EacBypassRandomFileName}.");
        //Console.WriteLine($"Переименован {RustInjectorOriginalFileName} в {RustInjectorRandomFileName}.");
        Console.WriteLine($"Переименован {superiorityRustOriginalFileName} в {SYRustRandomFileName}.");

        // Открываем важную папку
        Console.WriteLine("Открываем главную папку...");
        Process.Start("explorer.exe", directoryPath);

        открытиеПрограммыПоНазванию(directoryPath + "\\" + EacBypassRandomFileName);
        //открытиеПрограммыПоНазванию(directoryPath + "\\" + RustInjectorRandomFileName);

        // Проверяем ввод пользователя для очистки
        Console.WriteLine("Напишите 'clear' для очистки:");
        string? command;

        // Цикл do-while, который продолжается до тех пор, пока пользователь не введет "clear"
        do
        {
            command = Console.ReadLine();
            if (command != null && command.StartsWith("clear", StringComparison.OrdinalIgnoreCase))
            {
                // Завершаем задачи программ
                KillProcessByName("chrome");
                KillProcessByName(EacBypassСохряненоеФайловоеИмя);
                
                // Чистка папок
                Console.WriteLine("Чистка папок...");
                ClearDirectory2(directoryPath);
                ClearDirectory2(superiorityDirectoryPath);
                Console.WriteLine("Папки очищены.");

                // Удаляем указанные файлы в папке "Документы"
                Console.WriteLine("Удаляем файлы из документов...");
                DeleteFilesInDocuments(documentsPath, eacFixFiles);
                Console.WriteLine("Файлы из документов очищены.");

                удаляемИсториюИСессииВкладокGoogleChrome();

                очисткаЛоговWindows();

                Console.WriteLine("Очистка успешна завершина.");
            }
            else if (command != null && command.StartsWith("folder", StringComparison.OrdinalIgnoreCase))
            {
                Process.Start("explorer.exe", directoryPath);
            }
            else
            {
                Console.WriteLine("Вы ввели неверную команду. напишите 'clear/folder'.");
            }

        } while (command == null || !command.StartsWith("clear", StringComparison.OrdinalIgnoreCase));
    }

    static async Task CS2(string directoryPath)
    {
        // Создаем важную папку
        Directory.CreateDirectory(directoryPath);

        // Определяем файлы с оригинальными именами
        string CS2InjectorOriginalFileName = "Inj.exe";
        string CS2InjectorSettingsOriginalFileName = "settings.xml";
        string CS2OsrsOriginalFileName = "Osirs.dll";

        // Скачиваем файлы с оригинальными именами
        Console.WriteLine("Скачиваем Injector...");
        await DownloadFileAsync("https://github.com/ghfakegh1337/StealthCore/raw/refs/heads/main/files/cs2/Inj.exe", Path.Combine(directoryPath, CS2InjectorOriginalFileName));
        Console.WriteLine("Injector скачен.");

        Console.WriteLine("Скачиваем настройки для Injector...");
        await DownloadFileAsync("https://github.com/ghfakegh1337/StealthCore/raw/refs/heads/main/files/cs2/settings.xml", Path.Combine(directoryPath, CS2InjectorSettingsOriginalFileName));
        Console.WriteLine("Настройки для Injector скачены.");

        Console.WriteLine("Скачиваем Osiris...");
        await DownloadFileAsync("https://github.com/ghfakegh1337/StealthCore/raw/refs/heads/main/files/cs2/Osirs.dll", Path.Combine(directoryPath, CS2OsrsOriginalFileName));
        Console.WriteLine("Osiris скачен.");

        // Переименовываем скачанные файлы в случайные имена
        string CS2InjectorСохряненоеФайловоеИмя = GenerateRandomFileName(10);
        string CS2InjectorRandomFileName = CS2InjectorСохряненоеФайловоеИмя + ".exe";
        string CS2OsrsRandomFileName = GenerateRandomFileName(10) + ".dll";
        
        File.Move(Path.Combine(directoryPath, CS2InjectorOriginalFileName), Path.Combine(directoryPath, CS2InjectorRandomFileName));
        File.Move(Path.Combine(directoryPath, CS2OsrsOriginalFileName), Path.Combine(directoryPath, CS2OsrsRandomFileName));
        
        Console.WriteLine($"Переименован {CS2InjectorOriginalFileName} в {CS2InjectorRandomFileName}.");
        Console.WriteLine($"Переименован {CS2OsrsOriginalFileName} в {CS2OsrsRandomFileName}.");

        // Открываем важную папку
        Console.WriteLine("Открываем главную папку...");
        Process.Start("explorer.exe", directoryPath);

        // Проверяем ввод пользователя для очистки
        Console.WriteLine("Напишите 'clear' для очистки:");
        Console.WriteLine("Напишите 'folder' для открытия папки с читом:");
        string? command;

        // Цикл do-while, который продолжается до тех пор, пока пользователь не введет "clear/folder"
        do
        {
            command = Console.ReadLine();
            if (command != null && command.StartsWith("clear", StringComparison.OrdinalIgnoreCase))
            {
                // Завершаем задачи программ
                KillProcessByName("chrome");
                KillProcessByName(CS2InjectorСохряненоеФайловоеИмя);
                
                // Чистка папок
                Console.WriteLine("Чистка папок...");
                ClearDirectory2(directoryPath);
                Console.WriteLine("Папки очищены.");

                удаляемИсториюИСессииВкладокGoogleChrome();

                очисткаЛоговWindows();

                Console.WriteLine("Очистка успешна завершина.");
            }
            else if (command != null && command.StartsWith("folder", StringComparison.OrdinalIgnoreCase))
            {
                Process.Start("explorer.exe", directoryPath);
            }
            else
            {
                Console.WriteLine("Вы ввели неверную команду. напишите 'clear/folder'.");
            }

        } while (command == null || !command.StartsWith("clear", StringComparison.OrdinalIgnoreCase));
    }

    static void удаляемИсториюИСессииВкладокGoogleChrome()
    {
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

        // Удаляем историю браузера Google Chrome
        Console.WriteLine("Очистка истории Google Chrome...");
        DeleteFile(chromeHistoryPath);
        DeleteFile(chromeHistoryJournalPath);
        DeleteFile(chromeGuestHistoryPath);
        DeleteFile(chromeGuestHistoryJournalPath);
        Console.WriteLine("История Google Chrome очищена.");

        // Удаляем файлы сессий и вкладок Google Chrome
        Console.WriteLine("Очистка файлы сессий и вкладок Google Chrome...");
        ClearFilesDirectory(sessionsPath);
        Console.WriteLine("Сессий и вкладоки были очищены.");
    }

    static void очисткаЛоговWindows()
    {
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
                    Console.WriteLine($"Файл удален: {filePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Не получилось удалить файл {filePath}: {ex.Message}");
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
                        Console.WriteLine($"Файл удален: {file}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Не получилось удалить файл {file}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при доступе к каталогу {directoryPath}: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine($"Папки {directoryPath} не существует.");
        }
    }

    static void DeleteFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            try
            {
                File.Delete(filePath);
                Console.WriteLine($"Файл удален: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не получилось удалить файл {filePath}: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine($"Файл не найден: {filePath}");
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
        Console.WriteLine("Очистка всех Windows логов...");
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
        Console.WriteLine("Сделано.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearShellBag()
    {
        Console.WriteLine("Чистка ShellBag истории...");
        DeleteRegistryKey(@"HKEY_CURRENT_USER\Software\Classes\Local Settings\Software\Microsoft\Windows\Shell\MuiCache");
        DeleteRegistryKey(@"HKEY_CURRENT_USER\Software\Classes\Local Settings\Software\Microsoft\Windows\Shell\BagMRU");
        DeleteRegistryKey(@"HKEY_CURRENT_USER\Software\Classes\Local Settings\Software\Microsoft\Windows\Shell\Bags");
        DeleteRegistryKey(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\Shell\BagMRU");
        DeleteRegistryKey(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\Shell\Bags");
        Console.WriteLine("Сделано.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearExplorerHistory()
    {
        Console.WriteLine("Чистка Explorer истории...");
        DeleteRegistryKey(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\RunMRU");
        Console.WriteLine("Сделано.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearComDlg32()
    {
        Console.WriteLine("Чистка OpenSave и LastVisited истории...");
        DeleteRegistryKey(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32\FirstFolder");
        DeleteRegistryKey(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32\LastVisitedPidlMRU");
        DeleteRegistryKey(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32\LastVisitedPidlMRULegacy");
        DeleteRegistryKey(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32\OpenSavePidlMRU");
        Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32\OpenSavePidlMRU", "", "");
        Console.WriteLine("Сделано.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearUserAssist()
    {
        Console.WriteLine("Чистка UserAssist истории...");
        DeleteRegistryKey(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\UserAssist");
        Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\UserAssist", "", "");
        Console.WriteLine("Сделано.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearAppCompatCache()
    {
        Console.WriteLine("Чистка AppCompatCache истории...");
        DeleteRegistryKey(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\AppCompatCache");
        DeleteRegistryKey(@"HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\Control\Session Manager\AppCompatCache");
        Console.WriteLine("Сделано.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearDiagnosedApplications()
    {
        Console.WriteLine("Чистка DiagnosedApplications истории...");
        DeleteRegistryKey(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\RADAR\HeapLeakDetection\DiagnosedApplications");
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\RADAR\HeapLeakDetection\DiagnosedApplications", "", "");
        Console.WriteLine("Сделано.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearSearchHistory()
    {
        Console.WriteLine("Чистка Search истории...");
        string userSid = GetCurrentUserSid();
        DeleteRegistryKey($@"HKEY_USERS\{userSid}\Software\Microsoft\Windows\CurrentVersion\Search\RecentApps");
        Registry.SetValue($@"HKEY_USERS\{userSid}\Software\Microsoft\Windows\CurrentVersion\Search\RecentApps", "", "");
        Console.WriteLine("Сделано.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearBAM()
    {
        Console.WriteLine("Чистка BAM истории...");
        string userSid = GetCurrentUserSid();
        DeleteRegistryKey($@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\bam\UserSettings\{userSid}");
        DeleteRegistryKey($@"HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\Services\bam\UserSettings\{userSid}");
        Console.WriteLine("Сделано.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearAppCompatFlags()
    {
        Console.WriteLine("Чистка AppCompatFlags истории...");
        string userSid = GetCurrentUserSid();
        DeleteRegistryKey($@"HKEY_USERS\{userSid}\Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Compatibility Assistant\Store");
        DeleteRegistryKey($@"HKEY_USERS\{userSid}\Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers");
        Console.WriteLine("Сделано.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearMountedDevices()
    {
        Console.WriteLine("Чистка MountedDevices истории...");
        string userSid = GetCurrentUserSid();
        DeleteRegistryKey($@"HKEY_USERS\{userSid}\Software\Microsoft\Windows\CurrentVersion\Explorer\MountPoints2");
        Registry.SetValue($@"HKEY_USERS\{userSid}\Software\Microsoft\Windows\CurrentVersion\Explorer\MountPoints2", "", "");
        Console.WriteLine("Сделано.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearRecentFiles()
    {
        Console.WriteLine("Чистка Recent файлов истории...");
        string recentPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Microsoft\Windows\Recent");
        ClearDirectory(recentPath);
        Console.WriteLine("Сделано.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearPantherFiles()
    {
        Console.WriteLine("Чистка Panther файлов истории...");
        string pantherPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Panther");
        ClearDirectory(pantherPath);
        Console.WriteLine("Сделано.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearAppCompatFiles()
    {
        Console.WriteLine("Чистка AppCompat файлов истории...");
        string appCompatPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "appcompat");
        ClearDirectory(Path.Combine(appCompatPath, "Programs"));
        ClearDirectory(Path.Combine(appCompatPath, "Programs\\Install"));
        Console.WriteLine("Сделано.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearPrefetchFiles()
    {
        Console.WriteLine("Чистка Prefetch файлов истории...");
        string prefetchPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Prefetch");
        ClearDirectory(prefetchPath);
        Console.WriteLine("Сделано.");
    }

    [SupportedOSPlatform("windows")]
    static void ClearMinidumpFiles()
    {
        Console.WriteLine("Чистка Minidump файлов истории...");
        string minidumpPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Minidump");
        ClearDirectory(minidumpPath);
        Console.WriteLine("Сделано.");
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
            Console.WriteLine($"Не удалось удалить registry key: {ex.Message}");
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
            Console.WriteLine($"Не удалось удалить папку: {ex.Message}");
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
        Console.WriteLine($"Не удалось удалить папку: {ex.Message}");
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

    static void открытиеПрограммыПоНазванию(string имяФайла)
    {
        if(File.Exists(имяФайла))
        {
            try
            {
                Process.Start(имяФайла);
                Console.WriteLine($"Файл {имяФайла} запущен успешно.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при запуске файла {имяФайла}: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Указанный файл не существует.");
        }
    }
}