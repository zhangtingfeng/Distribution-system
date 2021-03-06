var playbox = (function () {
    var a = function () {
        var b = this;
        b.box = null;
        b.player = null;
        b.src = null;
        b.on = false;
        b.autoPlayFix = {
            on: true,
            evtName: ("ontouchstart" in window) ? "touchend" : "click"
        }
    };
    a.prototype = {
        init: function (b) {
            this.box = "string" === typeof (b) ? document.getElementById(b) : b;
            this.player = document.getElementById("audio");
            this.src = this.player.getAttribute("src");
            this.init = function () {
                return this
            };
            this.autoPlayEvt(true);
            return this
        },
        play: function () {
            if (this.autoPlayFix.on) {
                this.autoPlayFix.on = false;
                this.autoPlayEvt(false)
            }
            this.on = !this.on;
            if (true == this.on) {
                this.player.src = this.src;
                this.player.play()
            } else {
                this.player.pause();
                this.player.src = null
            }
            if ("function" == typeof (this.play_fn)) {
                this.play_fn.call(this)
            }
        },
        handleEvent: function (b) {
            if (b.target == this.box) {
                return
            }
            this.play()
        },
        autoPlayEvt: function (b) {
            if (b || this.autoPlayFix.on) {
                document.body.addEventListener(this.autoPlayFix.evtName, this, false)
            } else {
                document.body.removeEventListener(this.autoPlayFix.evtName, this, false)
            }
        }
    };
    return new a()
})();
playbox.play_fn = function () {
    this.box.className = this.on ? "btn_music on" : "btn_music";
    this.box.attr("class", this.box.className)
};