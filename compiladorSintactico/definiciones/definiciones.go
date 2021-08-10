package definiciones

type Token struct {
	TypeLexema int
	Lexema     string
	NumFila    int
	NumCol     int
}

type Nodo struct {
	TokenType Token  `json:"Tipo,omitempty"`
	Dtype	  int
	AtrValor  string `json:"Name,omitempty"`
	Izq       *Nodo  `json:"Izquierda,omitempty"`
	Med       *Nodo  `json:"Medio,omitempty"`
	Der       *Nodo  `json:"Derecha,omitempty"`
	Bro       *Nodo  `json:"Bro,omitempty"`
}

type Simbolo struct{
	Dtype int
	Valor string
}
