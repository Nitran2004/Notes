namespace Notes.Models;

internal class MZNote
{
    public string mzFilename { get; set; }
    public string mzText { get; set; }
    public DateTime mzDate { get; set; }

    public MZNote()
    {
        mzFilename = $"{Path.GetRandomFileName()}.notes.txt";
        mzDate = DateTime.Now;
        mzText = "";
    }

    public void Save() =>
    File.WriteAllText(System.IO.Path.Combine(FileSystem.AppDataDirectory, mzFilename), mzText);

    public void Delete() =>
        File.Delete(System.IO.Path.Combine(FileSystem.AppDataDirectory, mzFilename));

    public static MZNote Load(string filename)
    {
        filename = System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);

        if (!File.Exists(filename))
            throw new FileNotFoundException("Unable to find file on local storage.", filename);

        return
            new()
            {
                mzFilename = Path.GetFileName(filename),
                mzText = File.ReadAllText(filename),
                mzDate = File.GetLastWriteTime(filename)
            };
    }

    public static IEnumerable<MZNote> LoadAll()
    {
        // Get the folder where the notes are stored.
        string appDataPath = FileSystem.AppDataDirectory;

        // Use Linq extensions to load the *.notes.txt files.
        return Directory

                // Select the file names from the directory
                .EnumerateFiles(appDataPath, "*.notes.txt")

                // Each file name is used to load a note
                .Select(filename => MZNote.Load(Path.GetFileName(filename)))

                // With the final collection of notes, order them by date
                .OrderByDescending(note => note.mzDate);
    }

}