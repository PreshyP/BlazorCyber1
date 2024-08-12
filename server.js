const express = require('express');
const swaggerUi = require('swagger-ui-express');
const fs = require('fs');
const path = require('path');
const yaml = require('js-yaml');
const http = require('http');
const { Server } = require('socket.io');

const app = express();
const port = process.env.PORT || 3000; // Use environment variable or default to 3000

// Middleware to parse JSON bodies
app.use(express.json());

// Serve the Swagger documentation
const swaggerDocument = yaml.load(fs.readFileSync(path.join(__dirname, './swagger.yaml'), 'utf8'));
app.use('/api-docs', swaggerUi.serve, swaggerUi.setup(swaggerDocument));

// In-memory database (for simplicity)
let items = [];
let nextItemId = 1; // Track IDs for new items

// CRUD operations
// Create item
app.post('/items', (req, res) => {
    const item = req.body;
    item.id = nextItemId++; // Assign ID
    items.push(item);
    console.log('Item created:', item);
    res.status(201).send(item);
});

// Read all items
app.get('/items', (req, res) => {
    res.status(200).send(items);
});

// Read item by ID
app.get('/items/:id', (req, res) => {
    const id = parseInt(req.params.id);
    const item = items.find(i => i.id === id);
    if (item) {
        res.status(200).send(item);
    } else {
        res.status(404).send({ message: 'Item not found' });
    }
});

// Update item
app.put('/items/:id', (req, res) => {
    const id = parseInt(req.params.id);
    const index = items.findIndex(i => i.id === id);
    if (index !== -1) {
        items[index] = req.body;
        console.log('Item updated:', items[index]);
        res.status(200).send(items[index]);
    } else {
        res.status(404).send({ message: 'Item not found' });
    }
});

// Delete item
app.delete('/items/:id', (req, res) => {
    const id = parseInt(req.params.id);
    const index = items.findIndex(i => i.id === id);
    if (index !== -1) {
        items.splice(index, 1);
        console.log('Item deleted:', id);
        res.status(204).send();
    } else {
        res.status(404).send({ message: 'Item not found' });
    }
});

// Root route handler
app.get('/', (req, res) => {
    res.send('Welcome to my API server');
});

// Create HTTP server
const server = http.createServer(app);

// Create socket.io server and attach it to HTTP server
const io = new Server(server, {
    cors: {
        origin: "*",
        methods: ["GET", "POST"]
    }
});

// Socket.IO event handlers
io.on('connection', (socket) => {
    console.log('Client connected');

    socket.on('SendMessage', (user, message) => {
        console.log(`Received message from ${user}: ${message}`);
        io.emit('ReceiveMessage', user, message);
    });

    socket.on('disconnect', () => {
        console.log('Client disconnected');
    });
});

// Start the server
server.listen(port, () => {
    console.log(`Server is running on http://localhost:${port}`);
    console.log(`Swagger UI is available at http://localhost:${port}/api-docs`);
});

