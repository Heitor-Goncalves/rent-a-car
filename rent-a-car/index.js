const express = require('express');
const cors = require('cors');
const {DefaultAzureCredential} = require('@azure/identity');
const {ServiceBusClient} = require('@azure/service-bus');
require('dotenv').config();

const app = express();
app.use(cors());
app.use(express.json());

app.post('/api/locacao', async (req, res) => {
    const {nome, email, modelo, ano, tempoAluguel} = req.body;
    const connectionString = "Endpoint=sb://sb-heitor-cndcentral.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=VmGUOdarVPxqKjemhQDdKOOwZQ7uTryhX+ASbPjk3a4=";

    const mensgaem = {
        nome,
        email,
        modelo,
        ano,
        tempoAluguel,
        data: new Date().toISOString()
    };

    try {
        const crendential = new DefaultAzureCredential();
        const serviceBusConnection = connectionString;
        const quenueName = "fila-locacao-auto";
        const sbClient = new ServiceBusClient(serviceBusConnection);
        const sender = sbClient.createSender(quenueName);
        const menssage = {
            body: mensgaem,
            contentType: "application/json",
            subject: "Locação de Veículo"
        };

        await sender.sendMessages(menssage);
        await sender.close();
        await sbClient.close();

        res.status(201).json({message: "Mensagem enviada com sucesso!"});
        
    } catch (error) {
        console.error("Erro ao enviar mensagem:", error);
        return res.status(500).json({error: "Erro ao enviar mensagem"});
        
    }
})

app.listen(3000, () => {
    console.log("Servidor rodando na porta 3000");
})