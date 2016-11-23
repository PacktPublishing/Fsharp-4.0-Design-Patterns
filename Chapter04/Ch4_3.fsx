let verifyGuid g =
    match System.Guid.TryParse g with
    | (true,_ as r) -> sprintf "%s is a genuine GUID %A" g (snd r)
    | (_,_ as r) -> sprintf "%s is a garbage GUID, defaults to %A"
                        g (snd r)

verifyGuid "7dbb5967e90142c59690275bd10331f3"
verifyGuid "xyzzy"
