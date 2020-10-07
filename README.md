![ata logo](https://i.postimg.cc/YqF7Rh0T/atahelper.png)

    ATAHelper version 0.0.2.1007
        
## 💡Introduction

This tool is able of unzip and download files via command lines

## ❓How ATAHelper works?

- Download file
```bash
ATAHelper d <Url> <Filename with extension>
```   
- Unzip file
```bash
ATAHelper e <Directory> <Output Directory>
```  
- Uninstall system app
```bash
ATAHelper apkS <filename>
```  
To use this command you have to create a file with the list of apps, to create this file you can use this command
```bash
adb shell pm list packages -s > <filename>
```  
- Uninstall non system app
```bash
ATAHelper apkNS <filename>
```  
To use this command you have to create a file with the list of apps, to create this file you can use this command
```bash
adb shell pm list packages -3 > <filename>
```  
## 🌞Windows Requirements

1.	.NET Framework 4.7.2 needed!

