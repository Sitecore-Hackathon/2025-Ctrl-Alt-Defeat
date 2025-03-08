![Hackathon Logo](docs/images/hackathon.png?raw=true "Hackathon Logo")
# Sitecore Hackathon 2025

## Team name
Ctrl + Alt + Defeat

## Category
AI and Error Logs

## Description
Sitecore developers often spend valuable time manually sifting through log files to diagnose errors. Our AI-powered utility simplifies this process, providing instant insights and solutions, making error debugging more efficient and effortless.

## Module Purpose
This utility fetches the latest Sitecore log file, filters out warnings and errors, and leverages ChatGPT-4.0 to analyze them.

## Problem Solved
Manually diagnosing Sitecore errors can be time-consuming and complex. Developers often have to sift through large log files to identify and understand issues.

## Solution
Our tool automates this process by:
1. Fetching the latest Sitecore log file.
2. Filtering relevant error and warning messages.
3. Sending these logs to ChatGPT-4.0 for analysis.
4. Generating a structured report with detailed error explanations and potential solutions.
5. Storing the analyzed data in a separate file for easy reference.

## Key Benefits
- **Time-saving**: Automates error identification and resolution.
- **AI-powered insights**: Provides intelligent explanations and solutions.
- **Easy to use**: Fetch, analyze, and review logs effortlessly.
- **Better debugging efficiency**: Developers can focus on fixing issues instead of searching for them.

## Video link
[Demo Link](https://horizontal-my.sharepoint.com/:v:/p/ajha/EW_KQMBVO3dJuMLmg19oZigB8DLL1POdUCGbDgCYHsHMjw?e=UnFFn4)

## Pre-requisites and Dependencies
- Sitecore XM or XP
- .NET Framework (for running the utility)
- OpenAI API (for ChatGPT-4.0 integration)

## Installation instructions
1. Download and extract the utility files.
2. Ensure .NET Framework is installed on the system.
3. Configure the OpenAI API key in the web.config.
4. Update the configuration file with the Sitecore log directory path.
5. Hit the utility API through Postman to start analyzing Sitecore logs. Please find the attached Postman package [here](/docs/postman-package/Sitecore-Hackthon-2025.postman_collection.json)

## Configuration
- Update the configuration file with the Sitecore log directory path.
- Add OpenAI API credentials in web.config to enable log analysis.
```xml
{
    <add key="LogDirectory" value="logs" />
    <add key="APIKey" value="##########" />
}
```

## Usage instructions
1. Execute the utility to fetch and analyze the latest Sitecore logs.
2. Review the generated AI-powered report for detailed error explanations and possible fixes.
3. Use the provided solutions to debug and resolve Sitecore issues efficiently.

## Example Input/Output
The utility will generate an output file containing structured insights like:

![Example Output 1](/docs/images/Output_1.png)
![Example Output 2](/docs/images/Output_2.png)

## Future Scope
To further enhance usability and automation, the utility can be extended with the following features:
- Scheduler Integration: The utility can be configured to run at scheduled intervals, automatically fetching and analyzing logs without manual intervention.
- Sitecore Ribbon Button: A button can be added to the Sitecore ribbon, allowing developers to manually trigger log analysis from within the Sitecore interface, making it even more accessible and user-friendly.

## Comments
This tool streamlines Sitecore error debugging by leveraging AI, reducing the manual effort required for log analysis and troubleshooting. It enhances productivity and helps developers resolve issues faster, leading to improved system stability.