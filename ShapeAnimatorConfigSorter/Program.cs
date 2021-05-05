using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

var com3d2Path = Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\KISS\カスタムオーダーメイド3D2", "InstallPath", null) as string;
if (com3d2Path is null)
{
    com3d2Path = Environment.CurrentDirectory;
}
var configPath = Path.Combine(com3d2Path, @"Sybaris\UnityInjector\Config\ShapeAnimator.xml");
if (File.Exists(configPath))
{
    var xml = XDocument.Load(configPath);
    var items = xml.Root.Element("items");
    var sortedItems = items.Elements("item").OrderBy(x => x.Attribute("name").Value.Trim('*')).ThenBy(x => x.Attribute("tag").Value).ToArray();
    items.RemoveAll();
    foreach (var x in sortedItems)
    {
        items.Add(x);
    }
    var backupPath = configPath + ".bak";
    if (File.Exists(backupPath))
    {
        File.Delete(backupPath);
    }
    File.Copy(configPath, backupPath);
    xml.Save(configPath);
}
