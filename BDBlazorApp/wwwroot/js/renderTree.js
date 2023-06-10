let people = null;

function sketchTree(v) {
    let positions = [];

    function setRandomPos(min = 50, max = 450) {
        return Math.floor(Math.random() * (max - min)) + min;
    }

    function getPosition(c) {
        return positions.filter((p) => p.id === c.id)[0];
    }

    function getParentsPosition(c) {
        if (c.motherId !== -1 && c.fatherId !== -1) {
            return [
                ...positions.filter(
                    (p) => p.id === c.motherId || p.id === c.fatherId
                ),
            ];
        }
    }

    v.setup = function () {
        const canva = v.createCanvas(500, 500);
        canva.parent("tree-canvas");
        v.rectMode(v.CENTER);

        for (let p of people) {
            positions.push({
                ...p,
                x: setRandomPos(),
                y: setRandomPos(),
            });
        }
        console.log("SETUP");
    }
    console.log(people);
    v.draw = function () {
        v.background(0);

        for (let p of people) {
            const c = getPosition(p);

            const parents = getParentsPosition(p);

            if (parents) {
                for (let parent of parents) {
                    if (parent.gender === "F") {
                        v.stroke(255, 0, 0);
                    } else {
                        v.stroke(0, 0, 255);
                    }
                    v.line(c.x, c.y, parent.x, parent.y);
                    v.fill(255);
                    v.noStroke();
                    v.text("*", parent.x - 2.5, parent.y - 7.5);
                }
            }
        }
        for (let p of positions) {
            v.noStroke();
            if (p.gender === "F") {
                v.fill(255, 0, 0);
            } else {
                v.fill(0, 0, 255);
            }
            v.rect(p.x, p.y, 20, 20);
            v.fill(255);
            v.text(p.name, p.x, p.y);
        }
    }

}

window.renderTree = (dotNetHelper, data) => {
    people = [
        {
            id: 1,
            motherId: -1,
            fatherId: -1,
            name: "A",
            gender: "M",
        },
        {
            id: 2,
            motherId: -1,
            fatherId: -1,
            name: "B",
            gender: "F",
        },
        {
            id: 3,
            motherId: 1,
            fatherId: 2,
            name: "C",
            gender: "F",
        },
        {
            id: 4,
            motherId: -1,
            fatherId: -1,
            name: "D",
            gender: "M",
        },
        {
            id: 5,
            motherId: 3,
            fatherId: 4,
            name: "E",
            gender: "F",
        },
    ];
    dotNetHelper.invokeMethodAsync('GetHelloMessage', "ABC");
    new p5(sketchTree, '#tree-canvas');
}