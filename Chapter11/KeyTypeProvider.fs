namespace FSharp.IO.DesignTime

#nowarn "0025"

open System.Reflection
open System.IO
open Microsoft.FSharp.Core.CompilerServices
open ProviderImplementation.ProvidedTypes

[<TypeProvider>]
type public KeyStringProvider(config : TypeProviderConfig) as this = 
    inherit TypeProviderForNamespaces()

    let nameSpace = "FSharp.IO"
    let assembly = Assembly.LoadFrom(config.RuntimeAssembly)
    let providerType = ProvidedTypeDefinition(assembly, nameSpace, "SecretKey", baseType = None, HideObjectMethods = true)

    do
        providerType.DefineStaticParameters(
            parameters = [ ProvidedStaticParameter("Path", typeof<string>) ],             
            instantiationFunction = fun typeName [| :? string as path |] ->
                let t = ProvidedTypeDefinition(assembly, nameSpace, typeName, baseType = Some typeof<obj>, HideObjectMethods = true)
                let fullPath = if Path.IsPathRooted(path) then path else Path.Combine(config.ResolutionFolder, path)
                let content = File.ReadAllText(fullPath)
                t.AddMember <| ProvidedLiteralField("Key", typeof<string>, content)
                t
        )

        this.AddNamespace(nameSpace, [ providerType ])

[<assembly:TypeProviderAssembly()>]
do()