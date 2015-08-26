const
  VersionFileName = 'Version.defs';
  TemplateFileName = 'VersionInfoTemplate.cs';
  OutFileName = 'VersionInfo.cs';
  VersionTemplate = '{0} //Major' + System.Environment.NewLine + '{1} //Minor' + System.Environment.NewLine + '{2} //Revision';
var
  Major : integer:=0;
  Minor : integer:=1;
  Revision : integer:=0;
begin
  var VersionLines : array of string := System.IO.File.ReadAllLines(VersionFileName);
  var TemplateText : string := System.IO.File.ReadAllText(TemplateFileName);
  try
    integer.TryParse(VersionLines[0].Split(' ')[0], Major);
    integer.TryParse(VersionLines[1].Split(' ')[0], Minor);
    integer.TryParse(VersionLines[2].Split(' ')[0], Revision);
    Revision += 1;    
    var OutText:string := string.Format(TemplateText, Major, Minor, Revision);
    System.IO.File.WriteAllText(OutFileName, OutText);
    
    var UpdatedVersion: string := string.Format(VersionTemplate, Major, Minor, Revision);
    System.IO.File.WriteAllText(VersionFileName, UpdatedVersion);
  except
    Writeln('Ошибка в файле описания версии: ', VersionFileName );
  end;
end.