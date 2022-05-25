#region Load Environment File
string ENV_FILEPATH = Path.Combine(Directory.GetCurrentDirectory(), ".env");
if (!File.Exists(ENV_FILEPATH)) {
    Console.WriteLine("A .env file containing EMAIL and PASSWORD variables is required.");
    Environment.Exit(1);
}

foreach (var line in File.ReadAllLines(ENV_FILEPATH)) {
    var parts = line.Split('=', StringSplitOptions.RemoveEmptyEntries);

    if (parts.Length != 2)
        continue;

    Environment.SetEnvironmentVariable(parts[0], parts[1]);
}

#endregion

Console.WriteLine(Environment.GetEnvironmentVariable("EMAIL"));
Console.WriteLine(Environment.GetEnvironmentVariable("PASSWORD"));