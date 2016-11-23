// if-then as an expression
let f a b =  // a and b inferred of type 'a(requires comparison)
    if a < b then
        a
    else
        b

let f' a b =
    if a < b then
        a

let f'' (a:'a) b =
    if a < b then
        a // warning

let f''' (a:int) b =
    if a < b then
        a // error
