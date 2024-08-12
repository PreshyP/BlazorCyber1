
const io = new Server(server, {
    cors: {
        origin: "*",
        methods: ["GET", "POST"]
    }
});

io.on('connection', (socket) => {
    console.log('Client connected');

    socket.on('SendMessage', (user, message) => {
        io.emit('ReceiveMessage', user, message);
    });

    socket.on('disconnect', () => {
        console.log('Client disconnected');
    });
});
