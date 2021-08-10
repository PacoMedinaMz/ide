package logger

import (
	"encoding/json"
	"fmt"
	"os"
	"path/filepath"
	"runtime"
	"strings"

	d "X/definiciones"
)

type log struct {
	Lexico     []string
	Sintactico []string
	Semantico  []string
	Errores    []string
	Code       []string
	Tree       d.Nodo
}

var JsonLog = new(log)

func AgregarLexico(content string) {
	JsonLog.Lexico = append(JsonLog.Lexico, content)
}

func AgregarSintactico(content string) {
	JsonLog.Sintactico = append(JsonLog.Sintactico, content)
}

func AgregarSemantico(content string) {
	JsonLog.Semantico = append(JsonLog.Semantico, content)
}

func AgregarError(content string) {
	JsonLog.Errores = append(JsonLog.Errores, content)
}

func AgregarTree(content d.Nodo) {
	JsonLog.Tree = content
}

func AgregarCode(content string) {
	JsonLog.Code = append(JsonLog.Code, content)
}

func ConvertLogToJSON() string {
	var str string
	json, err := json.MarshalIndent(JsonLog, "", "    ")
	if err != nil {
		fmt.Println(err)
	} else {
		str = string(json)
		str = strings.ReplaceAll(str, "\\u003c", "<")
		str = strings.ReplaceAll(str, "\\u003e", ">")
		str = strings.ReplaceAll(str, "null", "[]")
	}
	return str
}

func CrearArchivo() {
	_, b, _, _ := runtime.Caller(0)
	basepath := filepath.Dir(b)
	path, err := filepath.Abs(basepath + "../../log/log.json")
	if err != nil {
		fmt.Println(err)
	} else {
		file, err := os.Create(path)
		if err != nil {
			fmt.Println(err)
			return
		}
		defer file.Close()
		_, err = file.WriteString(ConvertLogToJSON())
		if err != nil {
			fmt.Println(err)
		}
	}

}
