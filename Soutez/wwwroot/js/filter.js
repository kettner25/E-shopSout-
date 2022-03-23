var polozky = document.getElementsByClassName("polozka");
var ceny = document.getElementsByClassName("cena");

$(document).ready(function (){

    $(".kat").click(function () {
        let razeni = this.name;

        switch (razeni) {
            case "levny":
                SeradCena();
                break;
            case "drahy":
                SeradCena(false);
                break;
            case "abecedne": 
            
            break;
        }
    });
});

function SeradCena(odNejlevnejsi = true) {
    let body = document.querySelector("body");
    let content = document.getElementById("content")

    let poleCen = new Array();
    let _poleCen = new Array();
    let poleIndexu = new Array();

    for (let i = 0; i < ceny.length; i++) {
        poleCen.push(Number(String(ceny[i].innerHTML).split(" ").join("")));
    }
    _poleCen.push.apply(_poleCen, poleCen);

    if (odNejlevnejsi)
        poleCen.sort();
    else {
        poleCen.sort((a,b) => -1*(a - b));
    }

    console.log(poleCen);
    let zapsaneIndexy = new Array();
    for (let i = 0; i < poleCen.length; i++) {
        let _index_ = _poleCen.indexOf(poleCen[i]);

        if (zapsaneIndexy.indexOf(_index_) >= 0){
            let x = 0;

            while (zapsaneIndexy.indexOf(_index_) >= 0) {
                x++;   
                _index_ = _poleCen.indexOf(poleCen[i], x);
                console.log(_index_);
            }
        }
        zapsaneIndexy.push(_index_);
        poleIndexu.push(_index_);
    }

    let storage = document.createElement("div");
    storage.id = "storage"
    storage.innerHTML = content.innerHTML;

    body.appendChild(storage);

    console.log(poleIndexu);
    content.innerHTML = "";

    for (let i = 0; i < poleIndexu.length; i++){
        let _polozka = $(polozky).clone();
        content.appendChild(_polozka[poleIndexu[i]+i]);
    }

    body.removeChild(storage);
    /*
    polozky.sort(function(a, b) {
        return parseFloat(a.querySelector("#cena")) - parseFloat(b.querySelector("#cena"));
    });
    _storage.querySelectorAll(".polozka")[poleIndexu[i]]
    */
}