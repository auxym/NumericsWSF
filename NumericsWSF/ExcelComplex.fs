module ExcelComplex

// Excel doesn't handle complex numbers, so we return anything with a nonzero
// imaginary part as a string.

open System
open System.Numerics

type ExcelComplex = 
    | Complex of string
    | Real of float

let ofComplex (z:Complex) =
    match (abs z.Phase) % Math.PI  with
    | c when c < 1E-15 -> Complex (sprintf "%G+%Gi" z.Real z.Imaginary)
    | _ -> Real z.Real
 