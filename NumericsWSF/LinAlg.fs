module LinAlg

open ExcelDna.Integration
open ExcelDna.Documentation
open MathNet.Numerics.LinearAlgebra
open Utils

[<ExcelFunctionDoc(
    Name="NUM.LINALG.SOLVE",
    Category="Linear Algebra",
    Description="Solve the system of linear equations Ax=b for x using QR factorization.",
    Remarks="Based on MathNet.Numerics.Solve"
)>]
let solve
        ([<ExcelArgument(Description="Matrix A (size NxN)")>]
        A:float[,])

        ([<ExcelArgument(Description="Vector b (length N)")>]
        b:float[]) =
    
    // Validate input
    if not (Array2D.length1 A = Array2D.length2 A
            && Array2D.length1 A = Array.length b
            ) then failwith ""
    
    let Amat = CreateMatrix.DenseOfArray A
    let xv = CreateVector.DenseOfArray b
    Amat.Solve(xv).AsArray() |> packForCaller
