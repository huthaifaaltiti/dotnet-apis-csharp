// netlify-functions/getUsersInfo.js

const fetch = require("node-fetch");

exports.handler = async function (event, context) {
  try {
    const response = await fetch(
      "/.netlify/functions/getUsersInfo"
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
