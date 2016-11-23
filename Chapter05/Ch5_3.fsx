
type Configuration = {
    Database: string
    RetryCount: int
}

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>] 
[<AutoOpen>]
module Configuration =
    let private singleton = ref { Database  = "(local)"; RetryCount = 3 }
    let private guard = obj()

    type Configuration with
        static member Current 
            with get() = lock guard <| fun() -> !singleton
            and set value = lock guard <| fun() -> singleton := value


printfn "Default start-up config: %A" Configuration.Current

Configuration.Current <- { Configuration.Current with Database = ".\SQLExpress" }

printfn "Updated config: %A" Configuration.Current