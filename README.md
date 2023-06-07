## **Assignment details :**


![image](https://github.com/manoj191291/CodilityTask/assets/135921531/d8dd1750-9e0d-43f7-a8e0-c5a32b28a2a8)


## **Framework Details**
| Automation Framework| |
| ------------- | ------------- |
| Tool  | Selenium WebDriver |
|Proramming Language |c#|
| BDD  | Specflow  |
|IDE | Visual Studio|
| Test Framework | MSTest|
| Reporting | Allure |


## Execution:
- To run tests, use command "dotnet test 'path of solution or csproj'" from powershell/CMD/ VS Developer tools or open this project in VS and run through Test explorer
- Results are generated in .csv file under results folder. Each test will generate unique test csv file with rates.


## Execution Report:
After execution all execution results are generate in JSON files, these JSON files can be used to generate reports.

To generate reports, you need allure-commandline and java, add bin folder of allure and JAVA HOME in system path. 

Use following cmd commands to generate allure reports :

 - allure serve 'path of json files'
 
 - allure generate 'path of json file' -o 'path where reports needs to be generated'
 
 
 To Open existing execution report :
 
- allure open 'path of the report'

## What Can be improved :

- Parallel Execution : Framework can be further extended to have parallel execution capablity

 - Integration with cloud services like browserstack for compatiblity testing

- Logs : I havent added logger library, it can be implmented using log4net. Option to add logs is must all frameworks

- Screenshot in reports : I havent added code to capture screenshot but can be done using hooks easily , all we need to do is add following code
allureinstance.Addattachment("path of file");

- Integration with Test Management tool : I would have add integration with JIRA or Azure DevOps if time would have permitted to enable automatic update of execution status

- API integration
