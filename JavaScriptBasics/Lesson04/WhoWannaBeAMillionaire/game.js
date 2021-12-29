'use strict'

let game = {
    /**
     * Этот метод выполняется при запуске
     */
    init() {
        renderer.welcomeMessage();
    },

    play(){
        do {
            let player = new Player()
            for (let turn = 1; turn<=5; turn++) {
                let question = questionDB.getQuestion();
                renderer.printQuestion(question);
                let userAnswer = this.readAnswerFromUser();
                if (userAnswer == null) {return;}
                if (userAnswer == question.rightAnswer){
                    player.score++;
                }
            }
            renderer.showScore(player);
        } while (this.readYesNo('Хотите продолжить(Yes/No)?'))
    },

    readAnswerFromUser(){
        let n;
        do {
            let input = prompt("Ваш ответ(0-закончить игру):");
            n = parseInt(input);
            if (n===0) {
                return null;
            }
        } while (isNaN(n) || n<1 || n>4)
        return n;
    },

    readYesNo(question) {
        while(true) {
            let input = prompt(question);
            if (input === 'yes' || input==='no') {
                return input === 'yes'
            }
        }
    }
}

game.init()
game.play()