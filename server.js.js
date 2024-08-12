// server.js
const express = require('express');
const swaggerUi = require('swagger-ui-express');
const YAML = require('yamljs');
const path = require('path');
const app = express();
const port = 3000;

// Middleware to parse JSON bodies
app.use(express.json());

// Load the Swagger document
const swaggerDocument = YAML.load(path.join(__dirname, './swagger.yaml'));

// Serve the Swagger documentation
app.use('/api-docs', swaggerUi.serve, swaggerUi.setup(swaggerDocument));

// In-memory database (for simplicity)
let items = [];

// CRUD operations
// Create
app.post('/items', (req, res) => {
    const item = req.body;
    items.push(item);
    console.log('Item created:', item);
    res.status(201).send(item);
});

// Read
app.get('/items', (req, res) => {
    res.status(200).send(items);
});

app.get('/items/:id', (req, res) => {
    const id = parseInt(req.params.id);
    const item = items.find(i => i.id === id);
    if (item) {
        res.status(200).send(item);
    } else {
        res.status(404).send({ message: 'Item not found' });
    }
});

// Update
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

// Delete
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

// Start the server
app.listen(port, () => {
    console.log(`Server is running on http://localhost:${port}`);
    console.log(`Swagger UI is available at http://localhost:${port}/api-docs`);
});

