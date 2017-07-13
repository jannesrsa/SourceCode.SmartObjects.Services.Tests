[Reflection.Assembly]::LoadWithPartialName("System.Xml.Linq") | Out-Null

$files = Get-ChildItem -Path C:\ -Filter vstest.console.exe.config -Recurse -ErrorAction SilentlyContinue -Force
foreach ($file in $files) {
    $vstestConfigXDocument = [System.Xml.Linq.XDocument]::Load($file.FullName)
    if(!$vstestConfigXDocument.Root.Element("startup"))
    {
         $startupXElement = [System.Xml.Linq.XElement]::Parse('<startup useLegacyV2RuntimeActivationPolicy="true"> 
                <supportedRuntime version="v4.2"/>
            </startup>')

        $file.FullName
        $configSectionsXElement = $vstestConfigXDocument.Root.Element("configSections")
       
        if($configSectionsXElement)
        {
            $configSectionsXElement.AddAfterSelf($startupXElement)
        }
        else
        {
            $vstestConfigXDocument.Root.Add($startupXElement)
        }

        $vstestConfigXDocument.Save($file.FullName)
    }
}