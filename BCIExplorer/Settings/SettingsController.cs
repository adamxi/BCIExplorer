using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

public static class SettingsController {
    private static string folder = "Settings";
    private static string folderPath;
    private static string fileName_local;
    private static string fileName_global;
    private static string defaultFilePath_local;
    private static string defaultFilePath_global;
    private static List<object> settings_local;
    private static List<object> settings_global;
    private static XmlRootAttribute rootElement;

    static SettingsController() {
        fileName_local =  "Settings.xml";
        fileName_global = "Global_Settings.xml";
        settings_local = new List<object>();
        settings_global = new List<object>();
        rootElement = new XmlRootAttribute("settings");
        SetSettingsPath();
    }

    public static string FolderPath {
        get { return folderPath; }
    }

	public static string SettingsFileName {
		get { return fileName_local; }
	}

	public static string SettingsFilePath {
		get { return defaultFilePath_local; }
	}

    private static void SetSettingsPath() {
        folderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + folder + Path.DirectorySeparatorChar;
        defaultFilePath_local = folderPath + fileName_local;
        defaultFilePath_global = folderPath + fileName_global;
    }

    public static void AddClass(ISettings o, SettingsType type) {
        switch(type) {
            case SettingsType.local:
                settings_local.Add(o);
                break;
            case SettingsType.global:
                settings_global.Add(o);
                break;
        }
    }

    public static bool HasSettings(SettingsType type) {
        switch(type) {
            case SettingsType.local:
                return File.Exists(defaultFilePath_local);

            case SettingsType.global:
                return File.Exists(defaultFilePath_global);

            default:
                return false;
        }
    }

    private static Type[] GetTypes(List<object> settings) {
		Type[] types = new Type[settings.Count];

		for(int i = 0; i < settings.Count; i++) {
			types[i] = settings[i].GetType();
		}

		return types;
    }

    public static void Save() {
        Save(defaultFilePath_local, settings_local);
        Save(defaultFilePath_global, settings_global);
    }

    public static void Save(SettingsType type) {
        switch(type) {
            case SettingsType.local:
                Save(defaultFilePath_local, settings_local);
                break;

            case SettingsType.global:
                Save(defaultFilePath_global, settings_global);
                break;
        }
    }

    public static void Save(string fileName, SettingsType type) {
        switch(type) {
            case SettingsType.local:
                Save(fileName, settings_local);
                break;

            case SettingsType.global:
                Save(fileName, settings_global);
                break;
        }
    }

    public static void Save(string filePath, List<object> settings) {
        try {
            if(!Directory.Exists(folderPath)) {
                Directory.CreateDirectory(folderPath);
            }
            if(settings.Count > 0) {
                XmlWriterSettings ws = new XmlWriterSettings();
                ws.Indent = true;
                
                using(XmlWriter writer = XmlWriter.Create(filePath, ws)) {
					XmlSerializer serializer = new XmlSerializer(typeof(List<object>), null, GetTypes(settings), rootElement, null);
                    serializer.Serialize(writer, settings);
                }
            }
        } catch(Exception ex) {
            MessageBox.Show(ex.ToString(), "Save exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Loads all the settings from the added settings files.
    /// IMPORTANT - Loading must be done before using any settings from the settings classes.
    /// Otherwhise the code will point to a different instance of the current settings file.
    /// </summary>
    public static void Load() {
        if(HasSettings(SettingsType.local)) {
            Load(SettingsType.local);
        }

        if(HasSettings(SettingsType.global)) {
            Load(SettingsType.global);
        }
    }

    public static void Load(SettingsType type) {
        switch(type) {
            case SettingsType.local:
                Load(defaultFilePath_local, settings_local);
                break;

            case SettingsType.global:
                Load(defaultFilePath_global, settings_global);
                break;
        }
    }

    public static void Load(string fileName, SettingsType type) {
        switch(type) {
            case SettingsType.local:
                Load(fileName, settings_local);
                break;

            case SettingsType.global:
                Load(fileName, settings_global);
                break;
        }
    }

    public static void Load(string filePath, List<object> settings) {
        try {
			List<object> tmpSettings = null;

            using(XmlReader reader = XmlReader.Create(filePath)) {
                XmlSerializer deserializer = new XmlSerializer(typeof(List<object>), null, GetTypes(settings), rootElement, null);
                tmpSettings = deserializer.Deserialize(reader) as List<object>;
            }

			for( int i = 0; i < tmpSettings.Count; i++ )
			{
				settings[ i ] = tmpSettings[ i ];
				( settings[ i ] as ISettings ).SetDefault( tmpSettings[ i ] as ISettings );
			}

        } catch(Exception ex) {
            MessageBox.Show(ex.ToString(), "Load exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public enum SettingsType {
        local,
        global,
    }
}