[Reflection.Assembly]::LoadWithPartialName("System.Xml.Linq") | Out-Null
  
$pathToConfig = "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe.config"
$xDoc = [System.Xml.Linq.XDocument]::Load($pathToConfig)
$configSections = $xDoc.Root.Element("configSections")

$startupXml = [System.Xml.Linq.XElement]::Parse('<startup useLegacyV2RuntimeActivationPolicy="true"> 
       <supportedRuntime version="v4.0"/>
  </startup>')

$configSections.AddAfterSelf($startupXml)
$xDoc.Save($pathToConfig)

$xDoc2 = [System.Xml.Linq.XDocument]::Load($pathToConfig)
$xDoc2.ToString()
