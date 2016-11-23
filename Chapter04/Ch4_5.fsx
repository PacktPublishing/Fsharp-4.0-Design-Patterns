open System

let validate keyA keyB =
    match (keyA,keyB) with
    | ("","") -> "both keys are empty"
    | (x,y) & (("",_) | (_,"")) -> sprintf "one key is empty: keyA = %s; keyB = %s" x y
    | (x,y) when x = y -> sprintf "both keys are not empty: keyA = keyB = %s" x
    | (x,y) -> sprintf "both keys are not empty: keyA = %s; keyB = %s" x y

validate "" ""
validate "abc" ""
validate "" "abc"
validate "42" "42"
validate "xyzzy" "kensentme"