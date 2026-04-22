# FileLogger

Lightweight C# helper for writing timestamped log messages to a text file.

This project provides a simple way to log informational, warning, and error messages without repeating file handling logic. It also supports optional daily log files and exception logging.

## Functionality

* Write log messages to a text file
* Automatic timestamps
* Basic log levels:

  * `INFO`
  * `WARNING`
  * `ERROR`
* Exception logging support
* Optional daily log files
* Automatically creates target directory if it does not exist

---

## Example

```csharp
var logger = new FileLogger(@"C:\Logs\app.log");

logger.Log("Application started.");
logger.LogWarning("Configuration file not found.");
logger.LogError("Database connection failed.");
```

---

## Exception example

```csharp
try
{
    int value = int.Parse("abc");
}
catch (Exception ex)
{
    logger.LogError(ex);
}
```

---

## Daily log files

Enable daily log files by passing `true` as the second parameter:

```csharp
var logger = new FileLogger(@"C:\Logs\app.log", true);

logger.Log("Daily log started.");
```

Example output file:

```text
app_2026-04-22.log
```

You can also pass a directory:

```csharp
var logger = new FileLogger(@"C:\Logs", true);
```

Result:

```text
log_2026-04-22.txt
```

---

## Example output

```text
[2026-04-22 11:10:05] [INFO] Application started.
[2026-04-22 11:10:06] [WARNING] Configuration file not found.
[2026-04-22 11:10:07] [ERROR] Database connection failed.
```

Exception example:

```text
[2026-04-22 11:10:10] [ERROR] Input string was not in a correct format.
System.FormatException: Input string was not in a correct format.
...
```

---

## Notes

* Works on Windows
* Uses `File.AppendAllText` with UTF-8 encoding
* Creates log directory automatically
* Designed for simple logging scenarios
* Not intended as a full logging framework

---

## Additional note

This project was created as a lightweight utility for everyday file-based logging.
