let mutable x = "I'm x"
let mutable y = x
y <- "I'm y"
sprintf "%s|%s" x y

let rx = ref "I'm rx"
let ry = rx
ry := "I'm ry"
sprintf "%s|%s" !rx !ry