window.signaturePad = {
    ctx: null,
    canvas: null,
    drawing: false,

    init: function (canvasId) {
        const c = document.getElementById(canvasId);
        if (!c) return;
        this.canvas = c;
        this.ctx = c.getContext('2d');
        this.ctx.lineWidth = 2;
        this.ctx.strokeStyle = '#1a1a1a';
        this.ctx.lineCap = 'round';
        this.drawing = false;

        c.addEventListener('mousedown',  this._start.bind(this));
        c.addEventListener('mousemove',  this._draw.bind(this));
        c.addEventListener('mouseup',    this._stop.bind(this));
        c.addEventListener('mouseleave', this._stop.bind(this));
        c.addEventListener('touchstart', this._touchStart.bind(this), { passive: false });
        c.addEventListener('touchmove',  this._touchMove.bind(this),  { passive: false });
        c.addEventListener('touchend',   this._stop.bind(this));
    },

    _pos: function (e) {
        const r = this.canvas.getBoundingClientRect();
        return { x: e.clientX - r.left, y: e.clientY - r.top };
    },

    _start: function (e) { this.drawing = true; const p = this._pos(e); this.ctx.beginPath(); this.ctx.moveTo(p.x, p.y); },
    _draw:  function (e) { if (!this.drawing) return; const p = this._pos(e); this.ctx.lineTo(p.x, p.y); this.ctx.stroke(); },
    _stop:  function ()  { this.drawing = false; },

    _touchStart: function (e) { e.preventDefault(); this._start(e.touches[0]); },
    _touchMove:  function (e) { e.preventDefault(); this._draw(e.touches[0]); },

    clear: function (canvasId) {
        const c = document.getElementById(canvasId);
        if (c) c.getContext('2d').clearRect(0, 0, c.width, c.height);
    },

    getDataUrl: function (canvasId) {
        const c = document.getElementById(canvasId);
        return c ? c.toDataURL('image/png') : null;
    },

    isEmpty: function (canvasId) {
        const c = document.getElementById(canvasId);
        if (!c) return true;
        const data = c.getContext('2d').getImageData(0, 0, c.width, c.height).data;
        return !data.some(v => v !== 0);
    }
};
