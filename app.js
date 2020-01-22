const StockAnalyzer = require('./src/businessLogic/stockAnalyzer');

const stockWatch = new StockAnalyzer();
stockWatch.startService();
