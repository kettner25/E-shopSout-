$(document).ready(function (){

    $("button").click(function () {
        let name = this.name;

        if (name == "koupit"){
            fetch(`/Site/PridejDoKosiku?id=${_id}`, {
                method: "POST"
            }).then(alert("Přidáno do košíku"), location.reload());
        }
    });
});
