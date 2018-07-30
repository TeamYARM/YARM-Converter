# YARM Converter [![Build status](https://ci.appveyor.com/api/projects/status/ve27k6oexib9neal/branch/dev?svg=true)](https://ci.appveyor.com/project/justinyoo/yarm-converter/branch/dev) [![](https://img.shields.io/nuget/dt/Yarm.Converters.svg)](https://www.nuget.org/packages/Yarm.Converters/) [![](https://img.shields.io/nuget/v/Yarm.Converters.svg)](https://www.nuget.org/packages/Yarm.Converters/) #

**YARM Converter** is to help [ARM](https://docs.microsoft.com/en-us/azure/azure-resource-manager/) template authoring in [YAML](http://yaml.org/). Even though ARM templates officially only support JSON format, YAML as a superset of JSON is much better for writing ARM templates that have complex structure.


## Usage ##

**YARM Converter** contains two string extension methods &ndash; `.ToYaml()` and `.ToJson()`.

```csharp
var sb = new StringBuilder();
sb.AppendLine("{");
sb.AppendLine("  \"$schema\": \"https://schema.management.azure.com/schemas/2015-01-01/deploymentTemplate.json#\",");
sb.AppendLine("  \"contentVersion\": \"1.0.0.0\",");
sb.AppendLine("  \"parameters\": {},");
sb.AppendLine("  \"variables\": {},");
sb.AppendLine("  \"resources\": [],");
sb.AppendLine("  \"outputs\": {}");
sb.AppendLine("}");

var json = sb.ToString();
var yaml = json.ToYaml();
```

```csharp
var sb = new StringBuilder();
sb.AppendLine("$schema: https://schema.management.azure.com/schemas/2015-01-01/deploymentTemplate.json#");
sb.AppendLine("contentVersion: 1.0.0.0");
sb.AppendLine("parameters: {}");
sb.AppendLine("variables: {}");
sb.AppendLine("resources: []");
sb.AppendLine("outputs: {}");

var yaml = sb.ToString();
var json = yaml.ToJson();
```


## Contribution ##

Your contributions are always welcome! All your work should be done in your forked repository. Once you finish your work with corresponding tests, please send us a pull request onto our `dev` branch for review.


## License ##

**YARM Converter** is released under [MIT License](http://opensource.org/licenses/MIT)

> The MIT License (MIT)
>
> Copyright (c) 2018 Team YARM
> 
> Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
> 
> The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
> 
> THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
