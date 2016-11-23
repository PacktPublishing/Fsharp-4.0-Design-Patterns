// This is a placeholder

// function argument contravariance
type T = interface end // base
type S() = interface T // an implementation
let f (x: T) = () // a function upon base
f(S()) // application to implementation does not need coercion!
