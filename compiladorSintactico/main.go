package main

import (
	"fmt"
	"os"
	"./logger"
	s "./sintactico"
	as "./semantico"
	"./intermedio"
)

func main() {
	fmt.Println("entre")
	if len(os.Args[1:]) != 0 {
		arguments := os.Args[1:]
		var file string = arguments[0]
		arbol := s.Parse(file)
		as.Semantico(arbol)
		codigoIntermedio.GenCode(arbol.Der)
		logger.AgregarCode("(halt,_,_,_)")
		logger.CrearArchivo()
	} else {
		fmt.Println("Vacío")
	}
	return
}
