# AppLauncher v472

**AppLauncher v472** is a lightweight Windows Forms application launcher built using .NET Framework 4.7.2. It allows users to organize and launch applications or files into user-defined groups with a clean, intuitive interface.

This version is designed for performance and compatibility, ensuring that it works on most Windows systems without requiring any additional runtime installations.

---

## 🌟 Features

- 🗂️ **Custom Grouping**: Organize your apps/files into groups for faster access.
- 🔍 **Search Functionality**: Quickly search and filter apps within the selected group.
- 📂 **Drag & Drop Support**: Easily add files by dragging them into the app.
- 🧠 **File Type Flexibility**: Supports `.exe`, `.lnk`, and **all file types**.
- 🧼 **Lightweight**: Minimal dependencies, small build size (~300KB without SQLite DLL).
- 🧱 **Built on .NET Framework 4.7.2**: Compatible with most modern Windows systems out-of-the-box.
- 🗑️ **Delete Support**: Quickly remove apps from a group.
- 📌 **Remembers Last Group**: Automatically loads the first group on startup if available.

---

## 📦 Files and Structure

```
AppViewer472/
├── AppViewer472.csproj       # Project file
├── Form1.cs                  # Main UI logic
├── Form1.Designer.cs         # Layout Ui
├── App.config                #
├── Form1.resx                #
├── packages.config           #
├── App.ico                   #
├── Program.cs                #
├── Properties/               # Folder
└── bin/Release/              # Build output

AppEditor472/
├── AppEditor472.csproj       # Project file
├── Form1.cs                  # Main UI logic
├── Form1.Designer.cs         # Layout Ui
├── App.config                #
├── Form1.resx                #
├── packages.config           #
├── App.ico                   #
├── Program.cs                #
├── Properties/               # Folder
└── bin/Release/              # Build output
```

> Note: The app requires `System.Data.SQLite.dll` to be present in the same folder as the `.exe` to function properly.


## 🛠️ Building the Project

> **Prerequisites**:  
> - Visual Studio 2022  
> - .NET Framework 4.7.2 Developer Pack

You can build the project using Visual Studio or via command line:

```bash
msbuild AppViewer472.csproj /p:Configuration=Release
msbuild AppEditor472.csproj /p:Configuration=Release
```

---

## 🏁 Requirements

- **Windows 10/11 or later**  
- **.NET Framework 4.7.2 or higher** (built-in on most modern Windows systems)

---

## 📄 License

This project is open-source.

---

## 🙌 Credits

Created with ❤️ by [AESMSIX].  
Built to simplify launching apps with grouped management.
