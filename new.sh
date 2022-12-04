folderName="$1"
projectName="$1"

dataStringMarker="<Compile Include=\"Program.fs\" \\/>"
dataExport="    <Content Include=\"data/*.*\">\\n      <CopyToOutputDirectory>Always</CopyToOutputDirectory>\\n    </Content>"
dataExport="$dataExport\n    <ProjectReference Include=\"..\\\\lib\\\\lib.fsproj\">\n      <Name>lib.fsproj</Name>\n    </ProjectReference>"

if [ -d "$folderName" ]; then
    echo "folderName '$folderName' already exists. No project was created"
    exit 1
fi

echo "Creating new project '$projectName' in folderName '$folderName'"

dotnet new console -lang "F#" -n "$projectName" -o "$folderName"
dotnet sln add "$folderName"

mkdir "./$folderName/data"
touch "./$folderName/data/source.txt"

# add data export to project file
awk -v replacement="$dataExport" "/$dataStringMarker/ { print; print replacement; next }1" "./$folderName/$projectName.fsproj" > temp
cat temp > "./$folderName/$projectName.fsproj"
rm temp

echo "open InputUtils

[<EntryPoint>]
let main argv =
    let input =
        System.IO.File.ReadLines (getFileName argv)

    0
" > "./$folderName/Program.fs"
