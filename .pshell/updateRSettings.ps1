param(
[Parameter(Mandatory=$true, ValueFromPipeline=$true)][string] $path_ToRunSettings,
[Parameter(Mandatory=$true, ValueFromPipeline=$true)][string] $instance_Branch,
[Parameter(Mandatory=$true, ValueFromPipeline=$true)][string] $ui_instance_Url,
[Parameter(Mandatory=$true, ValueFromPipeline=$true)][string] $api_instance_Url,
[Parameter(Mandatory=$true, ValueFromPipeline=$true)][string] $api_Key,
[Parameter(Mandatory=$true, ValueFromPipeline=$true)][string] $api_Token,
[Parameter(Mandatory=$false, ValueFromPipeline=$true)][string] $zephyr_IsPublish,
[Parameter(Mandatory=$false, ValueFromPipeline=$true)][string] $zephyr_ProjectKey,
[Parameter(Mandatory=$false, ValueFromPipeline=$true)][string] $zephyr_CycleName,
[Parameter(Mandatory=$false, ValueFromPipeline=$true)][string] $zephyr_CycleComment,
[Parameter(Mandatory=$false, ValueFromPipeline=$true)][string] $zephyr_Token
[Parameter(Mandatory=$false, ValueFromPipeline=$true)][string] $agent_TestsResultsFolder
)

$xml = [xml](Get-Content -Path $path_ToRunSettings)

$node = $xml.RunSettings.TestRunParameters.Parameter | where {$_.name -eq 'Branch'}
$node.Value = $instance_Branch
$node = $xml.RunSettings.TestRunParameters.Parameter | where {$_.name -eq 'InstanceUrl'}
$node.Value = $ui_instance_Url
$node = $xml.RunSettings.TestRunParameters.Parameter | where {$_.name -eq 'ApiInstanceUrl'}
$node.Value = $api_instance_Url
$node = $xml.RunSettings.TestRunParameters.Parameter | where {$_.name -eq 'ApiKey'}
$node.Value = $api_Key
$node = $xml.RunSettings.TestRunParameters.Parameter | where {$_.name -eq 'ApiToken'}
$node.Value = $api_Token
$node = $xml.RunSettings.TestRunParameters.Parameter | where {$_.name -eq 'PublishToZephyr'}
$node.Value = $zephyr_IsPublish
$node = $xml.RunSettings.TestRunParameters.Parameter | where {$_.name -eq 'ZephyrProjectKey'}
$node.Value = $zephyr_ProjectKey
$node = $xml.RunSettings.TestRunParameters.Parameter | where {$_.name -eq 'ZephyrCycleName'}
$node.Value = $zephyr_CycleName
$node = $xml.RunSettings.TestRunParameters.Parameter | where {$_.name -eq 'ZephyrCycleComment'}
$node.Value = $zephyr_CycleComment
$node = $xml.RunSettings.TestRunParameters.Parameter | where {$_.name -eq 'ZephyrToken'}
$node.Value = $zephyr_Token
$node = $xml.RunSettings.TestRunParameters.Parameter | where {$_.name -eq 'AgentTestsResultsFolder'}
$node.Value = $agent_TestsResultsFolder

$xml.save($path_ToRunSettings)




