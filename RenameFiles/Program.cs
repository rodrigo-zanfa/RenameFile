// verificar se não foram fornecidos parâmetros de linha de comando
using RenameFiles;

if (args.Length == 0)
{
    Console.WriteLine("Nenhum parâmetro de verificação fornecido. Corrigir e informar:");
    Console.WriteLine("    Validsoft01  --->  irá padronizar os arquivos para o 1º envio à Validsoft");
    Console.WriteLine("    Validsoft02  --->  irá padronizar os arquivos para o 2º envio à Validsoft");
    Console.WriteLine("    Validsoft03  --->  irá converter os arquivos de áudio (.mp3, .opus, .ogg, .wav) das pastas via ffmpeg usando codec pcm_s16le, áudio mono e renomeando para extensão *---ffmpeg.wav");
    Console.WriteLine("    Validsoft04  --->  irá converter os arquivos de áudio (somente os *---ffmpeg.wav) das pastas para Base64, gerando um aquivo *---base64.txt");
    return;
}


// verificar se existe algum parâmetro de linha de comando contendo "Validsoft01"
if (args.Any(arg => arg.Equals("Validsoft01", StringComparison.CurrentCultureIgnoreCase)))
{
    var folder = @"C:\RODRIGO\Validsoft\2º envio - OneDrive_2025-06-10\enrools e verif";


    #region Renomear arquivos das pastas

    Console.WriteLine("Renomeando arquivos das pastas...");

    // listar as pastas
    var directories = Directory.GetDirectories(folder);

    // percorrer todas as subpastas
    foreach (var directory in directories)
    {
        // listar os arquivos na subpasta
        var files = Directory.GetFiles(directory);

        // percorrer todos os arquivos
        foreach (var file in files)
        {
            // obter o nome do arquivo
            var fileName = Path.GetFileName(file);

            // verificar se o nome do arquivo contém caracteres inválidos
            if (fileName.Contains(" ", StringComparison.CurrentCultureIgnoreCase) ||
                fileName.Contains("(", StringComparison.CurrentCultureIgnoreCase) ||
                fileName.Contains(")", StringComparison.CurrentCultureIgnoreCase))
            {
                var newFileName = fileName.Replace(" ", "", StringComparison.CurrentCultureIgnoreCase);
                newFileName = newFileName.Replace("(", "", StringComparison.CurrentCultureIgnoreCase);
                newFileName = newFileName.Replace(")", "", StringComparison.CurrentCultureIgnoreCase);
                var newFilePath = Path.Combine(directory, newFileName);
                // renomear o arquivo
                File.Move(file, newFilePath);
            }
            // verificar se o nome do arquivo contém "enroll" (em minúsculo)
            else if (fileName.Contains("enroll"))
            {
                var newFileName = fileName.Replace("enroll", "Cad");
                var newFilePath = Path.Combine(directory, newFileName);
                // renomear o arquivo
                File.Move(file, newFilePath);
            }
            // verificar se o nome do arquivo contém "Enrool" ou "Enroll"
            else if (fileName.Contains("Enrool", StringComparison.CurrentCultureIgnoreCase) ||
                fileName.Contains("Enroll", StringComparison.CurrentCultureIgnoreCase))
            {
                var newFileName = fileName.Replace("Enrool", "Enroll", StringComparison.CurrentCultureIgnoreCase);
                newFileName = newFileName.Replace("enrool", "Enroll", StringComparison.CurrentCultureIgnoreCase);
                newFileName = newFileName.Replace("11", "1", StringComparison.CurrentCultureIgnoreCase);
                newFileName = newFileName.Replace("12", "2", StringComparison.CurrentCultureIgnoreCase);
                newFileName = newFileName.Replace("13", "3", StringComparison.CurrentCultureIgnoreCase);
                var newFilePath = Path.Combine(directory, newFileName);
                // renomear o arquivo
                File.Move(file, newFilePath);
            }
            // verificar se o nome do arquivo contém "Cad"
            else if (fileName.Contains("Cad", StringComparison.CurrentCultureIgnoreCase))
            {
                var newFileName = fileName.Replace("Cad", "Enroll", StringComparison.CurrentCultureIgnoreCase);
                var newFilePath = Path.Combine(directory, newFileName);
                // renomear o arquivo
                File.Move(file, newFilePath);
            }
            // verificar se o nome do arquivo contém "Verify"
            else if (fileName.Contains("Verify", StringComparison.CurrentCultureIgnoreCase))
            {
                var newFileName = fileName.Replace("verify", "Verify", StringComparison.CurrentCultureIgnoreCase);
                newFileName = newFileName.Replace("1", "", StringComparison.CurrentCultureIgnoreCase);
                var newFilePath = Path.Combine(directory, newFileName);
                // renomear o arquivo
                File.Move(file, newFilePath);
            }
            // verificar se o nome do arquivo contém "Verif"
            else if (fileName.Contains("Verif", StringComparison.CurrentCultureIgnoreCase))
            {
                var newFileName = fileName.Replace("verif", "Verify", StringComparison.CurrentCultureIgnoreCase);
                var newFilePath = Path.Combine(directory, newFileName);
                // renomear o arquivo
                File.Move(file, newFilePath);
            }
            else
            {
                // exibir mensagem de nada a ser feito
                Console.WriteLine($"  - Ignorado: {Path.Combine(directory, fileName)}");
            }
        }
    }

    Console.WriteLine("Renomeação concluída.");

    #endregion


    Thread.Sleep(2000);


    #region Verificar arquivos das pastas

    Console.WriteLine("");
    Console.WriteLine("Verificando arquivos das pastas...");

    // percorrer todas as subpastas
    foreach (var directory in directories)
    {
        // listar os arquivos na subpasta
        var files = Directory.GetFiles(directory);

        if (files.Length != 4)
        {
            Console.WriteLine($"  - A pasta {directory} não contém os 4 arquivos necessários.");
        }
        else
        {
            var fileName1 = Path.GetFileNameWithoutExtension(files[0]);
            var fileName2 = Path.GetFileNameWithoutExtension(files[1]);
            var fileName3 = Path.GetFileNameWithoutExtension(files[2]);
            var fileName4 = Path.GetFileNameWithoutExtension(files[3]);

            if (fileName1 != "Enroll1" ||
                fileName2 != "Enroll2" ||
                fileName3 != "Enroll3" ||
                fileName4 != "Verify")
                Console.WriteLine($"  - A pasta {directory} não contém os arquivos no padrão de nomes esperado.");
        }
    }

    Console.WriteLine("Verificação concluída.");

    #endregion


    Thread.Sleep(2000);


    #region Renomear pastas

    Console.WriteLine("");
    Console.WriteLine("Renomeando pastas...");

    // percorrer todas as subpastas
    foreach (var directory in directories)
    {
        // obter o nome da pasta
        var directoryName = Path.GetFileName(directory);

        // verificar se o nome da pasta contém "Teste"
        if (directoryName.Contains("Teste", StringComparison.CurrentCultureIgnoreCase))
        {
            var newDirectoryName = directoryName.Replace("Teste", "Person", StringComparison.CurrentCultureIgnoreCase);
            newDirectoryName = newDirectoryName.Replace(" ", "", StringComparison.CurrentCultureIgnoreCase);
            var newDirectoryPath = Path.Combine(Path.GetDirectoryName(directory), newDirectoryName);
            // renomear a pasta
            Directory.Move(directory, newDirectoryPath);
        }
    }

    Console.WriteLine("Renomeação concluída.");

    #endregion


    Thread.Sleep(2000);
}


// verificar se existe algum parâmetro de linha de comando contendo "Validsoft02"
if (args.Any(arg => arg.Equals("Validsoft02", StringComparison.CurrentCultureIgnoreCase)))
{
    var folder = @"C:\RODRIGO\Validsoft\4º envio - OneDrive_2025-06-12\Online_210 cad_verif";


    #region Renomear arquivos das pastas

    Console.WriteLine("Renomeando arquivos das pastas...");

    // listar as pastas
    var directories = Directory.GetDirectories(folder);

    // percorrer todas as subpastas
    foreach (var directory in directories)
    {
        // listar os arquivos na subpasta
        var files = Directory.GetFiles(directory);

        // percorrer todos os arquivos
        foreach (var file in files)
        {
            // obter o nome do arquivo
            var fileName = Path.GetFileName(file);

            // verificar se o nome do arquivo não é um guid válido
            if (!Guid.TryParse(Path.GetFileNameWithoutExtension(fileName), out _))
            {
                var newFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(fileName)}";
                var newFilePath = Path.Combine(directory, newFileName);
                // renomear o arquivo
                File.Move(file, newFilePath);
            }
            else
            {
                // exibir mensagem de nada a ser feito
                Console.WriteLine($"  - Ignorado: {Path.Combine(directory, fileName)}");
            }
        }
    }

    Console.WriteLine("Renomeação concluída.");

    #endregion


    Thread.Sleep(2000);


    #region Verificar arquivos das pastas

    Console.WriteLine("");
    Console.WriteLine("Verificando arquivos das pastas...");

    // percorrer todas as subpastas
    foreach (var directory in directories)
    {
        // listar os arquivos na subpasta
        var files = Directory.GetFiles(directory);

        if (files.Length != 4)
        {
            Console.WriteLine($"  - A pasta {directory} não contém os 4 arquivos necessários.");
        }
        else
        {
            var fileName1 = Path.GetFileNameWithoutExtension(files[0]);
            var fileName2 = Path.GetFileNameWithoutExtension(files[1]);
            var fileName3 = Path.GetFileNameWithoutExtension(files[2]);
            var fileName4 = Path.GetFileNameWithoutExtension(files[3]);

            if (!Guid.TryParse(Path.GetFileNameWithoutExtension(fileName1), out _) ||
                !Guid.TryParse(Path.GetFileNameWithoutExtension(fileName2), out _) ||
                !Guid.TryParse(Path.GetFileNameWithoutExtension(fileName3), out _) ||
                !Guid.TryParse(Path.GetFileNameWithoutExtension(fileName4), out _))
                Console.WriteLine($"  - A pasta {directory} não contém os arquivos no padrão de nomes esperado.");
        }
    }

    Console.WriteLine("Verificação concluída.");

    #endregion


    Thread.Sleep(2000);


    #region Renomear pastas

    Console.WriteLine("");
    Console.WriteLine("Renomeando pastas...");

    // percorrer todas as subpastas
    foreach (var directory in directories)
    {
        // obter o nome da pasta
        var directoryName = Path.GetFileName(directory);

        // verificar se o nome da pasta contém "Teste"
        if (!directoryName.Contains("Person", StringComparison.CurrentCultureIgnoreCase))
        {
            var newDirectoryName = $"Person{int.Parse(directoryName):000}";
            newDirectoryName = newDirectoryName.Replace(" ", "", StringComparison.CurrentCultureIgnoreCase);
            var newDirectoryPath = Path.Combine(Path.GetDirectoryName(directory), newDirectoryName);
            // renomear a pasta
            Directory.Move(directory, newDirectoryPath);
        }
    }

    Console.WriteLine("Renomeação concluída.");

    #endregion


    Thread.Sleep(2000);
}


// verificar se existe algum parâmetro de linha de comando contendo "Validsoft03"
if (args.Any(arg => arg.Equals("Validsoft03", StringComparison.CurrentCultureIgnoreCase)))
{
    var folder = @"C:\RODRIGO\Validsoft\AudiosTeste";


    #region Converter arquivos das pastas

    Console.WriteLine("Convertendo arquivos de áudio das pastas usando ffmpeg...");

    // listar as pastas
    var directories = Directory.GetDirectories(folder);

    // percorrer todas as subpastas
    foreach (var directory in directories)
    {
        // listar os arquivos na subpasta
        var files = Directory.GetFiles(directory);

        // percorrer todos os arquivos
        foreach (var file in files)
        {
            // se a extensão do arquivo não for .mp3, .opus, .ogg, .wav, ignorar
            var extension = Path.GetExtension(file).ToLowerInvariant();
            if (extension != ".mp3" && extension != ".opus" && extension != ".ogg" && extension != ".wav")
            {
                // exibir mensagem de nada a ser feito
                Console.WriteLine($"  - Ignorado: {Path.Combine(directory, Path.GetFileName(file))}");
                continue;
            }

            // obter o nome do arquivo
            var fileName = Path.GetFileName(file);

            // criar o caminho do novo arquivo
            var newFileName = $"{Path.GetFileNameWithoutExtension(fileName)}---ffmpeg.wav";
            var newFilePath = Path.Combine(directory, newFileName);

            var (exitCode, output, error) = $"ffmpeg -i \"{file}\" -acodec pcm_s16le -ac 1 \"{newFilePath}\"".Execute();
            if (exitCode != 0)
            {
                Console.WriteLine($"  - Erro na conversão do arquivo {fileName}: {exitCode} - {output} - {error}");
            }
        }
    }

    Console.WriteLine("Conversão usando ffmpeg concluída.");

    #endregion


    Thread.Sleep(2000);
}


// verificar se existe algum parâmetro de linha de comando contendo "Validsoft04"
if (args.Any(arg => arg.Equals("Validsoft04", StringComparison.CurrentCultureIgnoreCase)))
{
    var folder = @"C:\RODRIGO\Validsoft\AudiosTeste";


    #region Converter arquivos das pastas

    Console.WriteLine("Convertendo arquivos de áudio das pastas em Base64...");

    // listar as pastas
    var directories = Directory.GetDirectories(folder);

    // percorrer todas as subpastas
    foreach (var directory in directories)
    {
        // listar os arquivos na subpasta
        var files = Directory.GetFiles(directory);

        // percorrer todos os arquivos
        foreach (var file in files)
        {
            // se o nome do arquivo não contém "---ffmpeg.wav", ignorar
            if (!Path.GetFileName(file).Contains("---ffmpeg.wav"))
            {
                // exibir mensagem de nada a ser feito
                Console.WriteLine($"  - Ignorado: {Path.Combine(directory, Path.GetFileName(file))}");
                continue;
            }

            // obter o nome do arquivo
            var fileName = Path.GetFileName(file);

            // converter o arquivo para Base64
            var fileBytes = File.ReadAllBytes(file);
            var base64String = Convert.ToBase64String(fileBytes);

            // criar o caminho do novo arquivo
            var newFileName = $"{Path.GetFileNameWithoutExtension(fileName)}---base64.txt";
            var newFilePath = Path.Combine(directory, newFileName);

            // escrever o conteúdo Base64 no novo arquivo
            File.WriteAllText(newFilePath, base64String);
        }
    }

    Console.WriteLine("Conversão em Base64 concluída.");

    #endregion


    Thread.Sleep(2000);
}
