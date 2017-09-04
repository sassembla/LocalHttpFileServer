DOTNET=/Applications/Unity2017.1.0p5/Unity.app/Contents/NetCore/Sdk/dotnet

# この2つを良い感じにUnity Editorから行う必要がある。restoreはリセットでいいとして、Unity2017AppPathと、
# resetと、
# start とかか。
# stateはrunningとstopped
$DOTNET restore
$DOTNET run 127.0.0.1 8081 ../AssetBundles/

