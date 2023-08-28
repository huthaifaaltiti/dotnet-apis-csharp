// netlify-functions/getUserByNatId.js

const fetch = require("node-fetch");

exports.handler = async function (event, context) {
  const { userNatNum } = event.queryStringParameters;

  try {
    const response = await fetch(
      `/.netlify/functions/getUserByNatId?userNatNum=${userNatNum}`
    );
    const data = await response.json();

    return {
      statusCode: 200,
      body: JSON.stringify(data),
    };
  } catch (error) {
    return {
      statusCode: 500,
      body: JSON.stringify({ error: "Internal Server Error" }),
    };
  }
};
